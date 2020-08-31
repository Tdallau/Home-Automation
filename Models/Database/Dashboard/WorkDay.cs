using System;

namespace HomeAutomation.Models.Database.Dashboard
{
  public class WorkDay
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime BreakTime { get; set; }
    public int Salary { get; set; }
    public string Description { get; set; }
  }
}