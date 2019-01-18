using AssetTracker.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, IMapper mapper, ILogger<UserController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;

            try
            {
                var item = _mapper.Map<Model.User>(
                    await _service.GetByIdAsync(Convert.ToInt32(id)));

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}", Name = "UserGet")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = _mapper.Map<Model.User>(
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
