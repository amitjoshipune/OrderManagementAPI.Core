using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
{
    public interface IUserService
    {
        ServiceResult Register(RegisterDto userDto);
        ServiceResult Login(LoginDto userDto);
    }
}
