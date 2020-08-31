using System;
namespace HomeAutomation.Models.Database.Dashboard
{
  public class Payment
  {
    public int Id { get; set; }
    public DateTime Month { get; set; }
    public decimal Amount { get; set; }
  }
}