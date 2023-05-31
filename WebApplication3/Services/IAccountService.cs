using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
    }
}
