using HomeAutomation.Models.Database.MyRecipes;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Helpers.Contexts
{
  public class MyRecipesContext : DbContext
  {
    public DbSet<Recipe> Recipe { get; set; }
    public DbSet<FavoriteRecipe> FavoriteRecipe { get; set; }
    public DbSet<Link> Link { get; set; }
    public DbSet<Ingredient> Ingredient { get; set; }
    public DbSet<AmountType> AmountType { get; set; }
    public DbSet<RecipeAmount> RecipeAmount { get; set; }
    public MyRecipesContext(DbContextOptions<MyRecipesContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
  }
}