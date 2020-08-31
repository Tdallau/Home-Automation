using System;
using System.Collections.Generic;

namespace HomeAutomation.Areas.Dashboard.Models
{
  public class HouresInMonth
  {
    public IEnumerable<object> Data { get; set; }
    public IEnumerable<DateTime> Keys { get; set; }
  }
}