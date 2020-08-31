using System;

namespace HomeAutomation.Models.Database.MyCalender
{
  public class MyCalender
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string FileName { get; set; }
    public Guid OwnerId { get; set; }
    public bool DisplayPublic { get; set; }
    public string Password { get; set; }
  }
}