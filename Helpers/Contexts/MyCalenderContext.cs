using System.Globalization;
using HomeAutomation.Models.Database.MyCalender;
using Microsoft.EntityFrameworkCore;


namespace HomeAutomation.Helpers.Contexts
{
  public class MyCalenderContext : DbContext
  {
    public DbSet<MyCalender> Calendar { get; set; }
    public MyCalenderContext(DbContextOptions<MyCalenderContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
  }
}