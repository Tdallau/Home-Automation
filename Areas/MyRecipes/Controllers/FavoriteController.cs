using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyRecipes.Interfaces;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Areas.MyRecipes.Controllers
{
  [ApiController]
  [Authorize]
  [EnableCors("SiteCorsPolicy")]
  [Area("myrecipes")]
  [Route("[area]/[controller]")]
  public class FavoriteController : ControllerBase
  {
    private readonly IRecipeService _recipeService;

    public FavoriteController(IRecipeService recipeService)
    {
      _recipeService = recipeService;
    }
    [HttpPost("{recipeId}")]
    public async Task<ActionResult<Response<string>>> AddRecipe(int recipeId)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      return Ok(await _recipeService.SetFavoriteRecipe(user.UserId, recipeId));
    }
  }
}