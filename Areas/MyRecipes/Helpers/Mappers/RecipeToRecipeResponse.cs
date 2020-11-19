using System;
using System.Linq;
using HomeAutomation.Areas.MyRecipes.Interfaces;
using HomeAutomation.Areas.MyRecipes.Models;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Database.MyRecipes;

namespace HomeAutomation.Areas.MyRecipes.Helpers.Mappers
{
  public class RecipeToRecipeResponse : BaseMapper<Recipe, RecipeResponse>
  {
    private readonly MyRecipesContext _context;
    private readonly IMapper<Ingredient, IngredientResponse> _ingredientMapper;
    private readonly IMapper<Link, LinkResponse> _linkMapper;
    private readonly IMapper<RecipeInstruction, RecipeInstructionResponse> _recipeInstructionMapper;
    public RecipeToRecipeResponse(
      MyRecipesContext context, 
      IMapper<Link, LinkResponse> linkMapper, 
      IMapper<Ingredient, IngredientResponse> ingredientMapper,
      IMapper<RecipeInstruction, RecipeInstructionResponse> recipeInstructinMapper)
    {
      _context = context;
      _linkMapper = linkMapper;
      _ingredientMapper = ingredientMapper;
      _recipeInstructionMapper = recipeInstructinMapper;
    }

    public override RecipeResponse Convert(Recipe recipe, bool getOne, Guid? userId)
    {
      var recipeResponse = base.Convert(recipe, userId);


      if (getOne)
      {
        recipeResponse.Ingredients = _context.Ingredient.Where(x => x.RecipeId == recipe.Id).Select(x => _ingredientMapper.Convert(x, userId)).ToList();
        recipeResponse.Links = _context.Link.Where(x => x.RecipeId == recipe.Id).Select(x => _linkMapper.Convert(x, userId))?.ToList();
        recipeResponse.Images = _context.RecipeImage.Where(x => x.RecipeId == recipe.Id).Select(x => x.Image)?.ToList();
        recipeResponse.RecipeInstructions = _context.RecipeInstruction.Where(x => x.RecipeId == recipe.Id).Select(x => _recipeInstructionMapper.Convert(x, userId))?.ToList();
      }
      var favorite = _context.FavoriteRecipe.FirstOrDefault(x => x.UserId == userId && x.RecipeId == recipe.Id);
      recipeResponse.Favorite = favorite != null;
      var mainImage = _context.RecipeImage.FirstOrDefault(x => x.RecipeId == recipe.Id);
      recipeResponse.MainImage = mainImage?.Image;

      var amount = _context.RecipeAmount.FirstOrDefault(x => x.Id == recipe.RecipeAmountId);
      if (amount == null) return recipeResponse;
      if(amount.Max == null) {
        recipeResponse.Amount = $"{amount.Min} {_context.AmountType.FirstOrDefault(x => x.Id == amount.AmountTypeId)?.Name}";
      } else {
        recipeResponse.Amount = $"{amount.Min}-{amount.Max} {_context.AmountType.FirstOrDefault(x => x.Id == amount.AmountTypeId)?.Name}";
      }

      return recipeResponse;
    }
  }
}