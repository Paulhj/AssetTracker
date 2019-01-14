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
        private readonly IAssetService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(IAssetService service,
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
                var asset = await _service.GetById(id);

                var locations = _mapper.Map<IEnumerable<Model.AssetLocation>>(asset.AssetLocations);

                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
