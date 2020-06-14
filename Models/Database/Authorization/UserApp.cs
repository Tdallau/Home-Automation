using System;
using System.Text.Json.Serialization;

namespace HomeAutomation.Models.Database.Authorization
{
  public class UserApp
  {
    public Guid UserId { get; set; }
    public Guid AppId { get; set; }
    public bool Default { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; }
    [JsonIgnore]
    public virtual App App { get; set; }
  }
}