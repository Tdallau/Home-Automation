using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Interfaces;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<ResponseList<AppResponse>>>> GetMyApps()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      return Ok(await _userService.GetMyApps(user.UserId));
    }

    [HttpPost]
    public async Task<ActionResult<Response<string>>> SetDefaultApp([FromBody] AppRequest app)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      return Ok(await _userService.SetDefaultApp(user.UserId, app.AppId));
    }
  }
}