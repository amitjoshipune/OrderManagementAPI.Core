using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface IUserService
    {
        ServiceResult Register(UserDto userDto);
        ServiceResult Login(UserDto userDto);
    }
}