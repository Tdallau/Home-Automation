using System.Collections.Generic;
namespace HomeAutomation.Areas.MyCalender.Models
{
  public class SearchResponse
  {
    public IEnumerable<SearchItem> List { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalCalenders { get; set; }
  }

  public class SearchItem
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string MainCategoryName { get; set; }
    public string LanguageName { get; set; }
    public string Description { get; set; }
  }
}