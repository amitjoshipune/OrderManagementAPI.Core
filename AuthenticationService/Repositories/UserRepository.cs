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
        public ServiceResult LoginAsync(User user)
        {
            var existingUser = _dbContext.Users
            .FirstOrDefault(u => u.Username == user.Username 
            && u.Password == user.Password
            && (u.EmailId == user.EmailId || (u.EmailId == null && user.EmailId == null)));

            if (existingUser != null && !string.IsNullOrEmpty(existingUser.Username))
            {
                return new ServiceResult { Success = true, Message = "Login successful." };
            }

            return new ServiceResult { Success = false, Message = "Invalid username or password." };
           
        }

        public ServiceResult RegisterAsync(User user)
        {
             var existingUser = _dbContext.Users
            .FirstOrDefault(u => u.Username == user.Username 
            /*&& u.Password == user.Password*/
            && (u.EmailId == user.EmailId || (u.EmailId == null && user.EmailId == null)));

            if (existingUser != null && !string.IsNullOrEmpty(existingUser.Username))
            {
                return new ServiceResult { Success = false, Message = "Username already exists" };
            }
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return new ServiceResult { Success = true, Message = "User registered successfully." };
        }
    }
}
