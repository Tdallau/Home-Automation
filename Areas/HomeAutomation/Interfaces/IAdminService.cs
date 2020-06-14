using System.Threading.Tasks;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.Authorization;

namespace HomeAutomation.Interfaces
{
    public interface IAdminService
    {
         Task<Response<ResponseList<App>>> GetApps();
    }
}