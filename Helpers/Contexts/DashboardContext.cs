using HomeAutomation.Models.Database.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Helpers.Contexts
{
  public class DashboardContext : DbContext
  {
    public DbSet<WorkDay> WorkDay { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DashboardContext(DbContextOptions<DashboardContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
  }
}