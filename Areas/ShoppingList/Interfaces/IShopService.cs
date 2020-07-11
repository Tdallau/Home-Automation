using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Models.Database.ShoppingList;

namespace HomeAutomation.Areas.ShoppingList.Interfaces
{
    public interface IShopService
    {
         Task<IEnumerable<Shop>> GetShops(Guid shoppingGroupId);
         Task<Shop> CreateShop(Guid shoppingGroupId, ShopRequest shopRequest);
    }
}