using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Interfaces;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Database.ShoppingList;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.Areas.ShoppingList.Services
{
  public class ProductService : IProductService
  {
    private readonly MyShoppingListContext _context;
    public ProductService(MyShoppingListContext context)
    {
      _context = context;
    }

    public async Task<List<string>> AutoComplete(string text)
    {
        return await _context.Product.Where(x => x.Name.Substring(0, text.Length).ToLower() == text.ToLower()).Select(x => x.Name).ToListAsync();
    }

    public async Task<ProductForShop> AddProduct(ProductRequest productRequest)
    {
      if (String.IsNullOrWhiteSpace(productRequest.Name) || productRequest.ShopId == 0) return null;
      var product = await _context.Product.FirstOrDefaultAsync(x => x.Name == productRequest.Name);
      if (product == null)
      {
        product = new Product()
        {
          Name = productRequest.Name
        };
        await _context.AddAsync(product);
        await _context.SaveChangesAsync();
      }
      var shopProduct = new ShopProduct()
      {
        Amount = productRequest.Amount,
        Bought = false,
        ProductId = product.Id,
        ShopId = productRequest.ShopId
      };
      await _context.AddAsync(shopProduct);
      await _context.SaveChangesAsync();


      return new ProductForShop() {
          Name = product.Name,
          Id = product.Id,
          Amount = shopProduct.Amount,
          Bought = shopProduct.Bought,
          ShopProductId = shopProduct.Id,
      };
    }

    public async Task<ProductForShop> UpdateProduct(int shopProductId, int shopId)
    {
      var shopProduct = await _context.ShopProduct.FirstOrDefaultAsync(x => x.Id == shopProductId);
      var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == shopProduct.ProductId);
      if(shopProduct == null) return null;

      shopProduct.Bought = !shopProduct.Bought;
      _context.Update(shopProduct);
      await _context.SaveChangesAsync();

      return new ProductForShop() {
          Name = product.Name,
          Id = product.Id,
          Amount = shopProduct.Amount,
          Bought = shopProduct.Bought,
          ShopProductId = shopProduct.Id,
      };
    }
  }
}