using System;
using System.Collections.Generic;
using System.Linq;
using HomeAutomation.Areas.TwitterStayInformd.Interfaces;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Database.TwitterStayInformd;

namespace HomeAutomation.Areas.TwitterStayInformd.Services
{
  public class TimelineService : ITimelineService
  {
    private readonly TwitterStayInformdContext _context;
    public TimelineService(TwitterStayInformdContext context)
    {
      _context = context;
    }

    public IEnumerable<Tweet> GetTweets()
    {
      return new List<Tweet>();
    }
  }
}