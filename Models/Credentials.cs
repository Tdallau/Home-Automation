using System;
using System.ComponentModel.DataAnnotations;

namespace HomeAutomation.Models
{
  public class Credentials
  {
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    [Required]
    public Guid AppId { get; set; }
  }
}