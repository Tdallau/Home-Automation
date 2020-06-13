using System;
namespace HomeAutomation.Models.Database.MyRecipes
{
    public class FavoriteRecipe
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Guid UserId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
