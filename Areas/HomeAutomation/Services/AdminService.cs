using System.Threading.Tasks;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Interfaces;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Services
{
  public class AdminService : IAdminService
  {
    private readonly MainContext _context;

    public AdminService(MainContext context)
    {
      _context = context;
    }

    public async Task<Response<ResponseList<App>>> GetApps()
    {
      var list = await _context.App.ToListAsync();
      return new Response<ResponseList<App>>()
      {
        Data = new ResponseList<App>()
        {
            List = list,
            Count = list.Count
        },
        Success = true
      };
    }
  }
}