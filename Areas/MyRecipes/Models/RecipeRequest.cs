using System.Collections.Generic;

namespace HomeAutomation.Areas.MyRecipes.Models
{
    public class RecipeRequest
    {
        public string Name { get; set; }
        public string VideoId { get; set; }
        public string Description { get; set; }
        public bool Private { get; set; }
        public bool Favorite { get; set; }
        public List<IngredientResponse> Ingredients { get; set; }
        public List<LinkResponse> Links { get; set; }
        public AmountRequest Amount { get; set; }
    }

    public class AmountRequest
    {
        public string Type { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
}