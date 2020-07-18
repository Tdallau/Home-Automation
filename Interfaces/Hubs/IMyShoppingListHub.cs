using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Models.Database.ShoppingList;

namespace HomeAutomation.Interfaces.Hubs
{
    public interface IMyShoppingListHub
    {
        // shop
         Task NewShopCreated(Shop shop);
         Task DeleteShop(int id);
         Task UpdateShop(int id);

        // products
        Task AddProduct(int shopId, ProductForShop product);
        Task UpdateProduct(int shopId, ProductForShop product);
        Task DeleteProduct(int shopId, int productId);
        Task MoveProduct(int oldShopId, int newShopId, int productId);

        // for response when app making call to server
        Task Echo(string function);
    }
}