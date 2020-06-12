using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Areas.MyRecipes.Controllers
{
  [ApiController]
  [Area("myrecipes")]
  [Route("[area]/[controller]")]
  public class RecipeController : ControllerBase
  {
    [HttpGet]
    public async Task<object> GetRecipes()
    {
      await Task.Delay(1000);
      return Ok(new
      {
        Message = "Dit is de my recipe test controller"
      });
    }
  }
}