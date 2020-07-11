using System;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Models.Database.ShoppingList;

namespace HomeAutomation.Areas.ShoppingList.Interfaces
{
  public interface IShoppingGroupService
  {
    Task<ShoppingGroup> GetShoppingGroupByUserId(Guid userId);
    Task<ShoppingGroup> CreateShoppingGroup(Guid userId, ShoppingGroupRequest shoppingGroupRequest);
    Task SetActiveShoppingGroup(Guid userId, ShoppingGroupRequest shoppingGroupRequest);
  }
}