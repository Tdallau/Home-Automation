using HomeAutomation.Models.Database.TwitterStayInformd;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Helpers.Contexts
{
  public class TwitterStayInformdContext : DbContext
  {
    public DbSet<Tweet> Tweet { get; set; }
    public DbSet<TwitterTask> TwitterTask { get; set; }
    public TwitterStayInformdContext(DbContextOptions<TwitterStayInformdContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
  }
}