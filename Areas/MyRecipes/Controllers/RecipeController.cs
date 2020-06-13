using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyRecipes.Interfaces;
using HomeAutomation.Areas.MyRecipes.Models;
using HomeAutomation.Models;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.MyRecipes;
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
  public class RecipeController : ControllerBase
  {
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
      _recipeService = recipeService;
    }
    // GET: api/Recipe
    [HttpGet]
    public async Task<ActionResult<List<RecipeResponse>>> GetRecipes()
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      return Ok(await _recipeService.GetRecipes(user.UserId));
    }

    // GET: api/Recipe/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<List<RecipeResponse>>> GetRecipeById(int id)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);


      return Ok(await _recipeService.GetRecipe(id, user.UserId));
    }

    [HttpPost]
    public async Task<ActionResult<RecipeResponse>> AddRecipe([FromBody] RecipeRequest recipeRequest)
    {
      var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      var userToken = token.Split(' ')[1];
      var user = UserToken.FromToken(userToken);

      return Ok(await _recipeService.AddRecipe(recipeRequest, user.UserId));
    }
  }
}