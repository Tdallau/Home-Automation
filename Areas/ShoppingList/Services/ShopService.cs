using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Interfaces;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Database.ShoppingList;
using Microsoft.EntityFrameworkCore;
using HomeAutomation.Areas.ShoppingList.Models;

namespace HomeAutomation.Areas.ShoppingList.Services
{
  public class ShopService : IShopService
  {
    private readonly MyShoppingListContext _context;
    public ShopService(MyShoppingListContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Shop>> GetShops(Guid shoppingGroupId)
    {
      var shops = await _context.Shop.Where(x => x.ShoppingGroupId == shoppingGroupId).ToListAsync();
      for (int i = 0; i < shops.Count(); i++)
      {
        var shop = shops.ElementAt(i);
        shop.Products = new List<ProductForShop>();
        var shopProducts = await _context.ShopProduct.Where(y => y.ShopId == shop.Id).ToListAsync();
        if (shopProducts.Count != 0)
        {
          foreach (var shopProduct in shopProducts)
          {
            var product = await _context.Product.FirstOrDefaultAsync(a => a.Id == shopProduct.ProductId);
            if (product != null)
            {
              shop.Products.Add(new ProductForShop()
              {
                Id = product.Id,
                Name = product.Name,
                Amount = shopProduct.Amount,
                Bought = shopProduct.Bought,
                ShopProductId = shopProduct.Id
              });
            }
          }
        }

      }
      return shops;
    }

    public async Task<Shop> CreateShop(Guid shoppingGroupId, ShopRequest shopRequest)
    {
        if(String.IsNullOrWhiteSpace(shopRequest.Name) || String.IsNullOrWhiteSpace(shopRequest.Logo)) return null;
        var shop = new Shop() {
            Logo = shopRequest.Logo,
            Name = shopRequest.Name,
            ShoppingGroupId = shoppingGroupId,
        };

        await _context.AddAsync(shop);
        await _context.SaveChangesAsync();
        return shop;
    }
  }
}