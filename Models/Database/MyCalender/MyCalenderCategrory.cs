using System.Globalization;
namespace HomeAutomation.Models.Database.MyCalender
{
  public class MyCalenderCategrory
  {
    public int CalenderId { get; set; }
    public int CategoryId { get; set; }

    public virtual MyCalender Calender { get; set; }
    public virtual Category Category { get; set; }
  }
}