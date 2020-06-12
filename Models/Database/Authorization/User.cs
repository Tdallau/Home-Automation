using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomeAutomation.Models.Database.Authorization
{
  public class User
  {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonIgnore]
    public virtual List<UserToken> UserTokens { get; set; }
    [JsonIgnore]
    public virtual List<UserApp> UserApps { get; set; }
  }
}