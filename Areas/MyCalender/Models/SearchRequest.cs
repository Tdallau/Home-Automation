namespace HomeAutomation.Areas.MyCalender.Models
{
  public class SearchRequest
  {
    public int Page { get; set; }
    public int CalendersPerPage { get; set; }
    public string SearchString { get; set; }
    public int CategoryId { get; set; }
  }
}