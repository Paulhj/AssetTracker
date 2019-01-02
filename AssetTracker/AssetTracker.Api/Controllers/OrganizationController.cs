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
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IOrganizationService service, IMapper mapper, ILogger<OrganizationController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var userId = 6; //TODO get this from Authentication

            try
            {
                var items = _mapper.Map<IEnumerable<Model.Organization>>(
                    await _service.GetByUserId(userId));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}", Name = "OrganizationGet")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = _mapper.Map<Model.Organization>(
                    _service.GetById(id));

                if (item == null)
                    return NotFound($"Organization with id:{id} was not found");

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
