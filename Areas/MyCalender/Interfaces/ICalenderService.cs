using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyCalender.Models;

namespace HomeAutomation.Areas.MyCalender.Interfaces
{
  public interface ICalenderService
  {
    Task<List<HomeAutomation.Models.Database.MyCalender.MyCalender>> GetCalenders(Guid? userId = null);
    Task<HomeAutomation.Models.Database.MyCalender.MyCalender> CreateNewCalender(MyCalenderRequest newCalender, Guid UserId);
    Task<SearchResponse> Search(SearchRequest searchRequest);
  }
}