using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Areas.Github.Controllers
{
  [ApiController]
  [Area("github")]
  [Route("[area]/[controller]")]
  public class TestController : ControllerBase
  {
    [HttpGet]
    public async Task<object> Login()
    {
      await Task.Delay(1000);
      return Ok(new
      {
        Message = "Dit is de github test controller"
      });
    }
  }
}