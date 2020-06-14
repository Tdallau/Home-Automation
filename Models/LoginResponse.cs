using System;

namespace HomeAutomation.Models
{
  public class LoginResponse
  {
    public JWTToken TokenSettings { get; set; }
    public Guid Id { get; set; }
    public Guid DefaultAppId { get; set; }
    public string DefaultAppName { get; set; }
    public string Email { get; set; }
  }
}