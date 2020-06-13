using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyRecipes.Interfaces;
using HomeAutomation.Areas.MyRecipes.Models;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Base;
using HomeAutomation.Models.Database.MyRecipes;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Areas.MyRecipes.Services
{
  public class RecipeService : IRecipeService
  {
    private readonly IMapper<Recipe, RecipeResponse> _mapper;
    private readonly MyRecipesContext _context;
    public RecipeService(IMapper<Recipe, RecipeResponse> mapper, MyRecipesContext context)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Response<ResponseList<RecipeResponse>>> GetRecipes(Guid userId)
    {
      var recipes = await _context.Recipe.Where(x => !x.Private).ToListAsync();
      return new Response<ResponseList<RecipeResponse>>()
      {
        Data = new ResponseList<RecipeResponse>() {
          List = recipes.Select(x => _mapper.Convert(x, false, userId)),
          Count = recipes.Count
        },
        Success = true
      };
    }

    public async Task<Response<RecipeResponse>> GetRecipe(int id, Guid userId)
    {
      var recipe = await _context.Recipe.FirstOrDefaultAsync(x => x.Id == id);
      return new Response<RecipeResponse>()
      {
        Success = true,
        Data = _mapper.Convert(recipe, true, userId)
      };
    }

    public async Task<Response<RecipeResponse>> AddRecipe(RecipeRequest recipeRequest, Guid userId)
    {
      var amount = new RecipeAmount() { AmountTypeId = await GetAmountTypeId(recipeRequest.Amount.Type), Min = recipeRequest.Amount.Min, Max = recipeRequest.Amount.Max };
      await _context.AddAsync(amount);
      await _context.SaveChangesAsync();

      var recipe = new Recipe()
      {
        Description = recipeRequest.Description,
        Name = recipeRequest.Name,
        Private = recipeRequest.Private,
        RecipeAmountId = amount.Id,
        UserId = userId,
        VideoId = recipeRequest.VideoId
      };
      await _context.AddAsync(recipe);
      await _context.SaveChangesAsync();

      if (recipeRequest.Ingredients != null)
      {
        var ingredients = recipeRequest.Ingredients.Select(x => new Ingredient()
        {
          Amount = x.Amount,
          Name = x.Name,
          RecipeId = recipe.Id,
          Unit = x.Unit
        });
        await _context.AddRangeAsync(ingredients);
      }
      if (recipeRequest.Links != null)
      {
        var links = recipeRequest.Links.Select(x => new Link()
        {
          RecipeId = recipe.Id,
          Url = x.Url
        });
        await _context.AddRangeAsync(links);
      }

      await _context.SaveChangesAsync();

      return new Response<RecipeResponse>()
      {
        Data = _mapper.Convert(recipe, false, userId),
        Success = true,
      };
      //var ingredients = recipeResponse.Ingredients.Select(x => new Ingredient() { RecipeId = recipeResponse })
    }

    private async Task<int> GetAmountTypeId(string name)
    {
      var amount = await _context.AmountType.FirstOrDefaultAsync(x => x.Name == name);
      if (amount == null)
      {
        amount = new AmountType() { Name = name };
        await _context.AddAsync(amount);
        await _context.SaveChangesAsync();
      }

      return amount.Id;
    }
  }

}