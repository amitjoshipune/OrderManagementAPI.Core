using AuthenticationService.Services;
using Azure.Messaging.ServiceBus;
using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;
        private readonly IAzureServiceBusClient _serviceBusClient;
        private readonly IConfiguration _configuration;
        private readonly string _usersQueueName;
        public UsersController(ICacheService cacheService, IUserService userService, IAzureServiceBusClient serviceBusClient , IConfiguration configuration)
        {
            _configuration = configuration;
            _cacheService = cacheService;
            _userService = userService;
            _serviceBusClient = serviceBusClient;
            _usersQueueName = _configuration.GetValue<string>("AzureServiceBusWithTopics:usersqueueName");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.Register(userDto);
            if (result.Success)
            {
                dynamic message = new ExpandoObject();
                message.Username = userDto.Username;
                message.Email = userDto.Email;
                message.Message = "successfully created";
                await _serviceBusClient.SendMessageAsync(_usersQueueName , JsonConvert.SerializeObject(message));

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

        //[HttpGet("GetUserById/{id}")]
        //public IActionResult GetUserById(string  id)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var usr = _userService.GetUserById(id);
        //    if(usr == null || usr.UserId == null)
        //    {
        //        return NotFound("User not found");
        //    }

        //    return Ok(usr);
        //}
    }
}
