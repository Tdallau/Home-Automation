using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Models.Database.ShoppingList;

namespace HomeAutomation.Areas.ShoppingList.Interfaces
{
  public interface IShoppingGroupService
  {
    Task<IEnumerable<ShoppingGroup>> GetAllShoppingGroupsByUserId(Guid userId);
    Task<ShoppingGroup> GetActiveShoppingGroupByUserId(Guid userId);
    Task<ShoppingGroup> CreateShoppingGroup(Guid userId, ShoppingGroupRequest shoppingGroupRequest);
    Task SetActiveShoppingGroup(Guid userId, ShoppingGroupRequest shoppingGroupRequest);
  }
}