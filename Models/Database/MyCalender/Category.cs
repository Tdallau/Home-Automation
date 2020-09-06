using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HomeAutomation.Models.Database.MyCalender
{
  public class Category
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }

    public virtual Category Parent { get; set; }

    public virtual List<MyCalenderCategrory> CalenderCategrories { get; set; }
  }
}