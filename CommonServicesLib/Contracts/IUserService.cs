using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
{
    public interface IUserService
    {
        ServiceResult Register(UserDto userDto);
        ServiceResult Login(UserDto userDto);
    }
}
