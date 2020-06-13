using System;

namespace HomeAutomation.Models
{
  public class LoginResponse
  {
    public JWTToken TokenSettings { get; set; }
    public Guid Id { get; set; }
    public string Email { get; set; }
  }
}