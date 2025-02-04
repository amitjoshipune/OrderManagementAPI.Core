using AuthenticationService.Data;
using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerDbContext _dbContext;
        public UserRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResult RegisterAsync(User user)
        {
            try
            {
                if (_dbContext.Users.Any(u => u.Username == user.Username))
                    return new ServiceResult { Success = false, Message = "Username is already taken." };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return new ServiceResult { Success = true, Message = "User registered successfully." };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Success = false, Message = ex.Message };
            }
        }
        public ServiceResult LoginAsync(User user)
        {
            try
            {
                var aUser = _dbContext.Users
                    .FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                if (user == null)
                    return new ServiceResult { Success = false, Message = "Invalid login attempt." };
                if (user.Username == null)
                    return new ServiceResult { Success = false, Message = "Invalid login attempt." };

                return new ServiceResult { Success = true, Message = "User logged in successfully." };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Success = false, Message = ex.Message };
            }
        }
    }
}
