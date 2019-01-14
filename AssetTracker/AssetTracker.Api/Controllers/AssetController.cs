using AssetTracker.Core.Criteria;
using AssetTracker.Core.Services;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AssetController : Controller
    {
        private readonly IAssetService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetController> _logger;

        public AssetController(IAssetService service,
            IUserService userService,
            IMapper mapper, 
            ILogger<AssetController> logger)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var organizationId = _userService.GetById(Convert.ToInt32(userId)).SelectedOrganizationId;
            var criteria = new AssetCriteria();

            try
            {
                var items = _mapper.Map<IEnumerable<Model.Asset>>(
                    await _service.GetByCriteria(Convert.ToInt32(organizationId), criteria));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}", Name = "AssetGet")]
        public async Task<IActionResult> Get(int id)
        {
            //Need to verify if user allowed to get this Asset

            try
            {
                var item = _mapper.Map<Model.Asset>(
                    await _service.GetById(id));

                if (item == null)
                    return NotFound($"User with id:{id} was not found");

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut()]
        [Authorize(Roles = "PowerUser")]
        public async Task<IActionResult> Put([FromBody]Model.AssetForUpdate model)
        {
            try
            {
                //Check to see if the model is Valid.  If not return the ModelState
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _logger.LogInformation($"Updating Asset with ID: {model.Id}");

                //Get the asset to modify and map properties
                var item = await _service.GetById(model.Id);

                if (item == null)
                    return NotFound($"Asset with ID:{model.Id} not found in database to update");

                item = _mapper.Map(model, item);

                if (await _service.Update(item))
                {
                    return Ok(_mapper.Map<Model.Asset>(
                        _service.GetById(model.Id)));
                }
            }
            catch (EntityException ex)
            {
                _logger.LogWarning($"Could not save Asset to the database due to following error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Asset: {ex}");
            }

            return BadRequest();
        }

        [HttpPost()]
        [Authorize(Roles = "PowerUser")]
        public async Task<IActionResult> Post([FromBody]Model.AssetForCreation model)
        {
            try
            {
                //Check to see if the model is Valid.  If not return the ModelState
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _logger.LogInformation("Creating new Asset");

                var userId = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
                var organizationId = _userService.GetById(Convert.ToInt32(userId)).SelectedOrganizationId;

                //Create new asset and map properties
                var newItem = new Core.Entities.Asset
                {
                    CreateDt = DateTime.Now
                };
                newItem = _mapper.Map<Core.Entities.Asset>(model);

                //Add Asset Location
                newItem.AssetLocations.Add(new Core.Entities.AssetLocation()
                {
                    LocationId = model.LocationId,
                    Asset = newItem,
                    Note = model.Note,
                    CreateDt = new DateTime(2018, 12, 4)
                });

                //Associate Asset with Organization
                newItem.AssetOrganizations.Add(new Core.Entities.AssetOrganization()
                {
                    OrganizationId = organizationId
                });

                if (await _service.Create(newItem))
                {
                    return Created(Url.Link("AssetGet",
                        new { id = newItem.Id }),
                            _mapper.Map<Core.Entities.Asset>(
                                _service.GetById(newItem.Id)));
                }
            }
            catch (EntityException ex)
            {
                _logger.LogWarning($"Could not save Asset to the database due to following error: {ex.Message}");
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Asset: {ex}");
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize("MustOwnAsset")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting asset with ID: {id}");

                var item = _service.GetById(id);

                if (item == null)
                    return NotFound($"Asset with ID:{id} not found in database to delete");

                if (await _service.Delete(id))
                {
                    return Ok();
                }
            }
            catch (EntityException ex)
            {
                _logger.LogWarning($"Could not delete Asset from the database due to following error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving Asset: {ex}");
            }

            return BadRequest();
        }
    }
}
