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
    [Route("api/asset/{id}/locations")]
    [Authorize]
    public class LocationsController : Controller
    {
        private readonly IAssetLocationService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(IAssetLocationService service,
            IUserService userService,
            IMapper mapper,
            ILogger<LocationsController> logger)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        [Authorize("MustOwnAsset")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<IEnumerable<Model.AssetLocation>>(
                        await _service.GetByAssetId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{locationId}", Name = "AssetLocationGet")]
        public async Task<IActionResult> Get(int id, int locationId, DateTime createDt)
        {
            try
            {
                var item = _mapper.Map<Model.AssetLocation>(
                    await _service.GetById(id, locationId, createDt));

                if (item == null)
                    return NotFound($"AssetLocation with id:{id},{locationId} was not found");

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize("MustOwnAsset")]
        public async Task<IActionResult> Post(int id, [FromBody]Model.AssetLocationForCreation model)
        {
            try
            {
                //Check to see if the model is Valid.  If not return the ModelState
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _logger.LogInformation("Creating new Asset Location Association");

                //Create new asset location and map properties
                var newItem = new Core.Entities.AssetLocation()
                {
                    AssetId = id,
                    LocationId = model.LocationId,
                    Note = model.Note,
                    CreateDt = DateTime.Now
                };

                if (await _service.Create(newItem))
                {
                    var url = Url.Link("AssetLocationGet",
                        new { id = newItem.AssetId,
                              locationId = newItem.LocationId,
                              createDt = newItem.CreateDt });

                    var ret = _service.GetById(newItem.AssetId, newItem.LocationId, newItem.CreateDt);
                    return Created(url, ret);
                }
            }
            catch (EntityException ex)
            {
                _logger.LogWarning($"Could not save AssetLocation to the database due to following error: {ex.Message}");
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception while saving AssetLocation: {ex}");
            }

            return BadRequest(ModelState);
        }
    }
}
