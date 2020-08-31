using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.Dashboard.Interfaces;
using HomeAutomation.Areas.Dashboard.Models;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Database.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Areas.Dashboard.Servcies
{
  public class WorkDayService : IWorkDayService
  {
    private readonly DashboardContext _context;
    public WorkDayService(DashboardContext context)
    {
      _context = context;
    }
    public async Task<WorkDay> CreateWorkDay(Guid userId, WorkDay workDay)
    {
      workDay.Salary = 11;
      workDay.UserId = userId;
      await _context.AddAsync(workDay);
      await _context.SaveChangesAsync();
      return workDay;
    }

    public async Task<HouresInMonth> GetHoursInMonth(Guid userId)
    {
      var data = await _context.WorkDay
          .Where(x => x.UserId == userId && x.Date.Year == DateTime.Now.Year)
          .ToListAsync();


      var table = data
        .OrderBy(x => x.Date)
        .GroupBy(x => new { x.Date.Year, x.Date.Month })
        .Select(x => new
        {
          Date = new DateTime(x.Key.Year, x.Key.Month, 1),
          Count = x.Sum(x => GetHours(x)),
        });
        
      return new HouresInMonth() {
          Data = table,
          Keys = table.Select(x => x.Date)
      };
    }

    public async Task<HouresInMonth> GetSalaryInMonth(Guid userId)
    {
      var data = await _context.WorkDay
          .Where(x => x.UserId == userId && x.Date.Year == DateTime.Now.Year)
          .ToListAsync();


      var table = data
        .OrderBy(x => x.Date)
        .GroupBy(x => new { x.Date.Year, x.Date.Month })
        .Select(x => new
        {
          Date = new DateTime(x.Key.Year, x.Key.Month, 1),
          Count = x.Sum(x => GetHours(x) * x.Salary),
          Payed = _context.Payment.FirstOrDefault(y => y.Month.Month == x.Key.Month && y.Month.Year == x.Key.Year).Amount
        });
        
      return new HouresInMonth() {
          Data = table,
          Keys = table.Select(x => x.Date)
      };
    }

    public async Task<IEnumerable<WorkDay>> GetWorkDays(Guid userId)
    {
      return await _context.WorkDay.Where(x => x.UserId == userId).OrderByDescending(x => x.Date).Take(10).ToListAsync();
    }

    private decimal GetHours(WorkDay x)
    {
      var startHour = x.StartTime.Hour;
      var startMinutes = x.StartTime.Minute;
      var endHour = x.EndTime.Hour;
      var endMinutes = x.EndTime.Minute;
      var breakHours = x.BreakTime.Hour;
      var breakMinutes = x.BreakTime.Minute;

      decimal breakTotal = breakHours + (breakMinutes / 60);
      decimal houres = endHour - startHour;
      decimal minutes = endMinutes - startMinutes;
      decimal total = houres + (minutes / 60);
      return Math.Round(total - breakTotal);
    }
  }
}