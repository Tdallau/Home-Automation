using HomeAutomation.Models.Database.ShoppingList;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Helpers.Contexts
{
  public class MyShoppingListContext : DbContext
  {
    public DbSet<ShoppingGroup> ShoppingGroup { get; set; }
    public DbSet<ShoppingGroupUser> ShoppingGroupUser { get; set; }
    public DbSet<Shop> Shop { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ShopProduct> ShopProduct { get; set; }

    public MyShoppingListContext(DbContextOptions<MyShoppingListContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
  }
}