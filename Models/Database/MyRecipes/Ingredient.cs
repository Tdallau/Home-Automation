using System;
using System.ComponentModel.DataAnnotations;

namespace HomeAutomation.Models.Database.MyRecipes
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
