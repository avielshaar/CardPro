using CardPro.Server.Models;

namespace CardPro.Server.Services
{
    public interface IUsersService
    {
        string GenerateJwtToken(User user);
        bool Login(User user);
    }
}