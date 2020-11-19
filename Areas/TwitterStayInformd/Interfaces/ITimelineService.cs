using System.Collections.Generic;
using HomeAutomation.Models.Database.TwitterStayInformd;

namespace HomeAutomation.Areas.TwitterStayInformd.Interfaces
{
    public interface ITimelineService
    {
         IEnumerable<Tweet> GetTweets();
    }
}