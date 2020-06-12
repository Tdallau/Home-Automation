using System.Threading.Tasks;
using HomeAutomation.Models;
using HomeAutomation.Models.Database.Authorization;

namespace HomeAutomation.Interfaces
{
    public interface IAuthorizationService
    {
         Task<User> Login(Credentials credentials);
         Task<bool> Register(Credentials credentials);
    }
}