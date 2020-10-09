using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeAutomation.Models.Database.MyRecipes
{
  public class Recipe
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string VideoId { get; set; }
    public string Instruction { get; set; }
    public string Description { get; set; }
    public bool Private { get; set; }

    public Guid UserId { get; set; }
    public int RecipeAmountId { get; set; }

    public bool Vega { get; set; }

    public virtual List<Ingredient> Ingredients { get; set; }
    public virtual List<Link> Links { get; set; }
    public virtual List<FavoriteRecipe> FavoriteRecipes { get; set; }
    public virtual List<RecipeImages> RecipeImages { get; set; }
    public virtual RecipeAmount RecipeAmount { get; set; }
  }
}
