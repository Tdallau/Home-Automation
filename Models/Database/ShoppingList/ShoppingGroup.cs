using System;
using System.ComponentModel.DataAnnotations;

namespace HomeAutomation.Models.Database.ShoppingList
{
  public class ShoppingGroup
  {
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
  }
}