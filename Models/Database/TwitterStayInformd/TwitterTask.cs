using System;

namespace HomeAutomation.Models.Database.TwitterStayInformd
{
  public class TwitterTask
  {
    public Guid Id { get; set; }
    public string User { get; set; }
    public string CronTime { get; set; }
    public int NumberOfTweets { get; set; } = 10;
    public string BodId { get; set; }
    public string ChatId { get; set; }
  }
}