using HomeAutomation.Models.Database.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Helpers.Contexts
{
  public class MainContext : DbContext
  {
    public DbSet<User> User { get; set; }
    public DbSet<App> App { get; set; }
    public DbSet<UserToken> UserToken { get; set; }
    public DbSet<UserApp> UserApp { get; set; }

    public MainContext(DbContextOptions<MainContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>()
          .HasIndex(u => u.Email)
          .IsUnique();
      builder.Entity<UserApp>()
        .HasKey(x => new { x.UserId, x.AppId });
    }
  }
}