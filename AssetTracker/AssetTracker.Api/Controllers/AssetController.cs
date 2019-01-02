using AssetTracker.Core.Criteria;
using AssetTracker.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : Controller
    {
        private readonly IAssetService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetController> _logger;

        public AssetController(IAssetService service, IMapper mapper, ILogger<AssetController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var organizationId = 5; //TODO get this from Authentication
            var criteria = new AssetCriteria();

            try
            {
                var items = _mapper.Map<IEnumerable<Model.Asset>>(
                    await _service.GetByCriteria(organizationId, criteria));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}", Name = "AssetGet")]
        public IActionResult Get(int id)
        {
            //Need to verify if user allowed to get this Asset

            try
            {
                var item = _mapper.Map<Model.Asset>(
                    _service.GetById(id));

                if (item == null)
                    return NotFound($"User with id:{id} was not found");

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
