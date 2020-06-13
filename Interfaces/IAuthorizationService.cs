using System.Threading.Tasks;
using HomeAutomation.Models;
using HomeAutomation.Models.Database.Authorization;

namespace HomeAutomation.Interfaces
{
    public interface IAuthorizationService
    {
         Task<LoginResponse> Login(Credentials credentials);
         Task<bool> Register(Credentials credentials);
         Task<JWTToken> Refresh(JWTToken tokens);

         Task Logout(string refreshToken);
    }
}