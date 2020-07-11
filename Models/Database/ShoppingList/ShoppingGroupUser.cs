using System;

namespace HomeAutomation.Models.Database.ShoppingList
{
  public class ShoppingGroupUser
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ShoppingGroupId { get; set; }
    public bool Active { get; set; }

    public ShoppingGroup ShoppingGroup { get; set; }
  }
}