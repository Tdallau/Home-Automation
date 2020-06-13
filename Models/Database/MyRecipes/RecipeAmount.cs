using System;
using System.ComponentModel.DataAnnotations;

namespace HomeAutomation.Models.Database.MyRecipes
{
    public class RecipeAmount
    {
        public int Id { get; set; }
        [Required]
        public int AmountTypeId { get; set; }
        [Required]
        public string Min { get; set; }
        public string Max { get; set; }

        public AmountType AmountType { get; set; }
    }
}
