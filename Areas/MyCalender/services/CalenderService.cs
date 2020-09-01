using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.MyCalender.Interfaces;
using HomeAutomation.Areas.MyCalender.Models;
using HomeAutomation.Helpers.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Areas.MyCalender.services
{
  public class CalenderService : ICalenderService
  {
    private readonly MyCalenderContext _context;
    private readonly IPasswordHasher<HomeAutomation.Models.Database.MyCalender.MyCalender> _passwordHasher;
    public CalenderService(MyCalenderContext context, IPasswordHasher<HomeAutomation.Models.Database.MyCalender.MyCalender> passwordHasher)
    {
      _context = context;
      _passwordHasher = passwordHasher;
    }

    public async Task<List<HomeAutomation.Models.Database.MyCalender.MyCalender>> GetCalenders()
    {
      var calenders = _context.Calendar.Where(x => x.DisplayPublic);
      await calenders.ForEachAsync(x => x.Password = null);
      return await calenders.ToListAsync();
    }

    public async Task<HomeAutomation.Models.Database.MyCalender.MyCalender> CreateNewCalender(MyCalenderRequest newCalender, Guid userId)
    {
      if (String.IsNullOrWhiteSpace(newCalender.Name) || String.IsNullOrWhiteSpace(newCalender.FileName))
      {
        return null;
      }
      var calender = new HomeAutomation.Models.Database.MyCalender.MyCalender()
      {
        Name = newCalender.Name,
        FileName = newCalender.FileName,
        DisplayPublic = true,
        OwnerId = userId,
      };

      if (!String.IsNullOrWhiteSpace(newCalender.Password))
      {
        calender.Password = _passwordHasher.HashPassword(calender, newCalender.Password);
      }

      await _context.AddAsync(calender);
      await _context.SaveChangesAsync();
      return calender;
    }
  }
}