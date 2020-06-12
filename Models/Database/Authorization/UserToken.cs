using System;
using System.Text.Json.Serialization;

namespace HomeAutomation.Models.Database.Authorization
{
  public class UserToken
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime ExpiryDate { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; }
  }
}