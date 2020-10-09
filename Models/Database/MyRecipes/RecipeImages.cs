namespace HomeAutomation.Models.Database.MyRecipes
{
  public class RecipeImages
  {
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Image { get; set; }

    public virtual Recipe Recipe { get; set; }
  }
}