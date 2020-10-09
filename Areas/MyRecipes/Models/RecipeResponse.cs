using System.Collections.Generic;

namespace HomeAutomation.Areas.MyRecipes.Models
{
  public class RecipeResponse
  {
    public int Id { get; set; }

    public string Name { get; set; }
    public string VideoId { get; set; }
    public string Amount { get; set; }
    public string Description { get; set; }
    public bool Private { get; set; }
    public bool Favorite { get; set; }
    public bool Vega { get; set; }
    public List<IngredientResponse> Ingredients { get; set; }
    public List<LinkResponse> Links { get; set; }
  }
}