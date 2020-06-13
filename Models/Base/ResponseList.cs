using System.Collections;
using System.Collections.Generic;
namespace HomeAutomation.Models.Base
{
  public class ResponseList<T>
  {
    public IEnumerable List { get; set; }
    public int Count { get; set; }
  }
}