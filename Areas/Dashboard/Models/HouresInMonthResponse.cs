using System;
using System.Collections.Generic;
using HomeAutomation.Models.Base;

namespace HomeAutomation.Areas.Dashboard.Models
{
  public class HouresInMonthResponse
  {
    public ResponseList<object> HouresList { get; set; }
    public IEnumerable<DateTime> Keys { get; set; }
  }
}