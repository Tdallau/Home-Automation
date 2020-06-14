using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyRecipes.Models;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.MyRecipes;

namespace HomeAutomation.Areas.MyRecipes.Interfaces
{
  public interface IRecipeService
  {
    Task<Response<ResponseList<RecipeResponse>>> GetRecipes(Guid userId);
    Task<Response<RecipeResponse>> GetRecipe(int id, Guid userId);
    Task<Response<RecipeResponse>> AddRecipe(RecipeRequest recipeRequest, Guid userId);

    // favorite
    Task<Response<string>> SetFavoriteRecipe(Guid userId, int recipeId);
  }
}