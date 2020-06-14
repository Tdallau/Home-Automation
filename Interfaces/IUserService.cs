using System;
using System.Threading.Tasks;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;

namespace HomeAutomation.Interfaces
{
    public interface IUserService
    {
         Task<Response<ResponseList<AppResponse>>> GetMyApps(Guid userId);
         Task<Response<string>> SetDefaultApp(Guid userId, Guid appId);
    }
}