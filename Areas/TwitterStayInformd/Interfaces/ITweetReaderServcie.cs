using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Areas.TwitterStayInformd.Modals;
using HomeAutomation.Models.Database.TwitterStayInformd;

namespace HomeAutomation.Areas.TwitterStayInformd.Interfaces
{
    public interface ITweetReaderServcie
    {
         Task<(string, bool, object)> Start(Guid taskId);
         void Stop(Guid taskId);
         Task<bool> CreateNewTask(TwitterTask task);
    }
}