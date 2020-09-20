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

    public async Task<List<HomeAutomation.Models.Database.MyCalender.MyCalender>> GetCalenders(Guid? userId = null)
    {
      IQueryable<HomeAutomation.Models.Database.MyCalender.MyCalender> calenders;
      if (userId != null)
      {
        calenders = _context.Calendar.Where(x => x.OwnerId == userId);
      }
      else
      {
        calenders = _context.Calendar.Where(x => x.DisplayPublic);
      }

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

    public async Task<SearchResponse> Search(SearchRequest searchRequest)
    {
      if (String.IsNullOrWhiteSpace(searchRequest.SearchString))
      {
        return null;
      }
      if (searchRequest.CalendersPerPage == 0) searchRequest.CalendersPerPage = 10;
      var calenders = await _context.Calendar
        .Where(x =>
          x.DisplayPublic &&
          x.Name
            .ToLower()
            .StartsWith(searchRequest.SearchString.ToLower()
          )
        )
        .ToListAsync();
      decimal numberOfPages = Math.Ceiling((decimal)calenders.Count() / (decimal)searchRequest.CalendersPerPage);
      if (searchRequest.Page > numberOfPages)
      {
        searchRequest.Page = (int)numberOfPages;
      }
      var responseList = calenders
          .Skip(searchRequest.CalendersPerPage * searchRequest.Page)
          .Take(searchRequest.CalendersPerPage)
          .Select(x =>
          {
            var language = _context.Language.FirstOrDefault(y => y.Id == x.LanguageId);
            var calenderCategrories = _context.CalenderCategrory.Where(y => y.CalenderId == x.Id);

            var mainCategory = _context.Category.FirstOrDefault(y => calenderCategrories.FirstOrDefault(z => z.CategoryId == y.Id) != null && y.ParentId == null);
            if (mainCategory != null && (searchRequest.CategoryId == 0 || searchRequest.CategoryId == mainCategory.Id))
            {
              return new SearchItem()
              {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                LanguageName = language == null ? null : language.Name,
                MainCategoryName = mainCategory.Name
              };
            }
            else
            {
              return null;
            }
          })
          .Where(x => x != null);

      numberOfPages = Math.Ceiling((decimal)responseList.Count() / (decimal)searchRequest.CalendersPerPage);

      return new SearchResponse()
      {
        TotalCalenders = responseList.Count(),
        Page = searchRequest.Page,
        TotalPages = (int)numberOfPages,
        List = responseList
      };
    }
  }
}