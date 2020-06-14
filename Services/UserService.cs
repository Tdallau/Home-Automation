using System;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Interfaces;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Services
{
  public class UserService : IUserService
  {
    private readonly MainContext _context;
    public UserService(MainContext context)
    {
      _context = context;
    }
    public async Task<Response<ResponseList<AppResponse>>> GetMyApps(Guid userId)
    {
      var myApps = await _context.UserApp.Where(userApp => userApp.UserId == userId).ToListAsync();
      return new Response<ResponseList<AppResponse>>()
      {
        Success = true,
        Data = new ResponseList<AppResponse>()
        {
          List = myApps.Select(x => new AppResponse()
          {
            Default = x.Default,
            Id = x.AppId,
            Name = _context.App.FirstOrDefault(app => app.Id == x.AppId).Name
          }),
          Count = myApps.Count()
        }
      };
    }

    public async Task<Response<string>> SetDefaultApp(Guid userId, Guid appId)
    {
      var apps = await _context.UserApp.Where(x => x.UserId == userId).ToListAsync();
      var newDefaultApp = apps.FirstOrDefault(x => x.AppId == appId);
      if (newDefaultApp == null) return new Response<string>()
      {
        Data = "app not found",
        Success = false
      };

      foreach (var app in apps)
      {
          app.Default = false;
      }
      newDefaultApp.Default = true;

      _context.UpdateRange(apps);
      await _context.SaveChangesAsync();

      return new Response<string>()
      {
        Data = "New default app",
        Success = true
      };
    }
  }
}