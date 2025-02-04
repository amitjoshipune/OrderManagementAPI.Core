using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>();
        private readonly IMemoryCache _cache;
        private readonly IUserRepository _userRepository;
        private const string CacheKey = "Users";

        public UserService(IUserRepository userRepository, IMemoryCache cache)
        {
            _cache = cache;
            _userRepository = userRepository;
        }

        public ServiceResult Register(RegisterDto userDto)
        {

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email,
                DateOfBirth = userDto.DateOfBirth
            };
            var serviceResult = _userRepository.RegisterAsync(user);
            return serviceResult;
        }

        public ServiceResult Login(LoginDto userDto)
        {
            

            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password
            };

            var serviceResult = _userRepository.LoginAsync(user);
            return serviceResult;
        }
    }
}
