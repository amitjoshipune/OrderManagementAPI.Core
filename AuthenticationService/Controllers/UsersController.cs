using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;

        public UsersController(ICacheService cacheService, IUserService userService)
        {
            _cacheService = cacheService;
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.Register(userDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.Login(userDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }
    }
}
