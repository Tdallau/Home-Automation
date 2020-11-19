namespace HomeAutomation.Models.Database.MyRecipes
{
    public class RecipeInstruction
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Step { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}