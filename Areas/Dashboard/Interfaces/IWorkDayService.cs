using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Models.Database.Dashboard;
using HomeAutomation.Areas.Dashboard.Models;

namespace HomeAutomation.Areas.Dashboard.Interfaces
{
    public interface IWorkDayService
    {
         Task<IEnumerable<WorkDay>> GetWorkDays(Guid userId);
         Task<WorkDay> CreateWorkDay(Guid userId, WorkDay workDay);
         Task<HouresInMonth> GetHoursInMonth(Guid userId);
         Task<HouresInMonth> GetSalaryInMonth(Guid userId);
    }
}