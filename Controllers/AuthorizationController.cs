using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class AuthorizationController : ControllerBase
  {
    [HttpGet]
    public async Task<object> Login()
    {
      await Task.Delay(1000);
      return Ok(new
      {
        Message = "Dit is de login route"
      });
    }
  }
}