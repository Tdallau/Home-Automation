using System.Threading.Tasks;
using HomeAutomation.Interfaces;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("automation")]
  [Route("[area]/[controller]")]
  public class AdminController : ControllerBase
  {
    private readonly IAdminService _adminService;

    public AdminController(IAdminService service)
    {
        _adminService = service;
    }

    [HttpGet]
    public async Task<ActionResult<Response<ResponseList<App>>>> getApps()
    {
        return Ok(await _adminService.GetApps());
    }
  }
}