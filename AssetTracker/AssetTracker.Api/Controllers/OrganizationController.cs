using AssetTracker.Core.Services;
using AutoMapper;
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
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IOrganizationService service,
            IUserService userService, 
            IMapper mapper, 
            ILogger<OrganizationController> logger)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var organization = _userService.GetById(Convert.ToInt32(userId));

            try
            {
                var items = _mapper.Map<IEnumerable<Model.Organization>>(
                    await _service.GetByUserId(Convert.ToInt32(userId)));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}", Name = "OrganizationGet")]
        [Authorize("MustBelongToOrganization")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var test = await _service.GetById(id);

                var item = _mapper.Map<Model.Organization>(
                    await _service.GetById(id));

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
