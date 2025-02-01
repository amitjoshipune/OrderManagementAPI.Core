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
        private const string CacheKey = "Users";

        public UserService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public ServiceResult Register(UserDto userDto)
        {
            if (_users.Any(u => u.Username == userDto.Username))
            {
                return new ServiceResult { Success = false, Message = "Username already exists" };
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = userDto.Username,
                Password = userDto.Password // Note: In a real application, always hash passwords
            };

            _users.Add(user);
            _cache.Set(CacheKey, _users);
            return new ServiceResult { Success = true, Message = "User registered successfully" };
        }

        public ServiceResult Login(UserDto userDto)
        {
            var user = _users.FirstOrDefault(u => u.Username == userDto.Username && u.Password == userDto.Password);
            if (user == null)
            {
                return new ServiceResult { Success = false, Message = "Invalid username or password" };
            }

            return new ServiceResult { Success = true, Message = "Login successful" };
        }
    }
}
