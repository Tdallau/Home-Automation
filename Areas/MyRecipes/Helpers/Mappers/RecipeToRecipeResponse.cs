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
    private readonly IMapper<RecipeImages, RecipeImageResponse> _imageMapper;
    public RecipeToRecipeResponse(MyRecipesContext context, IMapper<Link, LinkResponse> linkMapper, IMapper<Ingredient, IngredientResponse> ingredientMapper, IMapper<RecipeImages, RecipeImageResponse> imageMapper)
    {
      _context = context;
      _linkMapper = linkMapper;
      _ingredientMapper = ingredientMapper;
      _imageMapper = imageMapper;
    }

    public override RecipeResponse Convert(Recipe recipe, bool getOne, Guid? userId)
    {
      var recipeResponse = base.Convert(recipe, userId);


      if (getOne)
      {
        recipeResponse.Ingredients = _context.Ingredient.Where(x => x.RecipeId == recipe.Id).Select(x => _ingredientMapper.Convert(x, userId)).ToList();
        recipeResponse.Links = _context.Link.Where(x => x.RecipeId == recipe.Id).Select(x => _linkMapper.Convert(x, userId))?.ToList();
        recipeResponse.Images = _context.RecipeImage.Where(x => x.RecipeId == recipe.Id).Select(x => _imageMapper.Convert(x, userId))?.ToList();
      }
      var favorite = _context.FavoriteRecipe.FirstOrDefault(x => x.UserId == userId && x.RecipeId == recipe.Id);
      recipeResponse.Favorite = favorite != null;
      var mainImage = _context.RecipeImage.FirstOrDefault(x => x.RecipeId == recipe.Id);
      recipeResponse.MainImage = mainImage != null ? _imageMapper.Convert(mainImage, userId) : null;

      var amount = _context.RecipeAmount.FirstOrDefault(x => x.Id == recipe.RecipeAmountId);
      if (amount == null) return recipeResponse;
      recipeResponse.Amount = $"{amount.Min}-{amount.Max} {_context.AmountType.FirstOrDefault(x => x.Id == amount.AmountTypeId)?.Name}";

      return recipeResponse;
    }
  }
}