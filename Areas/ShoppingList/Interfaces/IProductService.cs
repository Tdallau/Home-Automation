using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Models;

namespace HomeAutomation.Areas.ShoppingList.Interfaces
{
    public interface IProductService
    {
         Task<List<string>> AutoComplete(string text);
         Task<ProductForShop> AddProduct(ProductRequest productRequest);
         Task<ProductForShop> UpdateProduct(int shopProductId, int shopId);
         Task<bool> MoveProduct(MoveProductRequest moveProductRequest);

         Task<bool> DeleteProduct(DeleteProductRequest deleteProductRequest);
    }
}