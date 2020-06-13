using System;
namespace HomeAutomation.Models.Database.MyRecipes
{
    public class Link
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Url { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
