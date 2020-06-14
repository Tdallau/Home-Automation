using System;

namespace HomeAutomation.Models
{
  public class AppResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Default { get; set; }
    public string Area { get; set; }
  }
}