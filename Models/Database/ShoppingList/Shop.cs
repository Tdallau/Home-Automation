using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Helpers;

namespace HomeAutomation.Models.Database.ShoppingList
{
  public class Shop
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Logo { get; set; }

    // FK -> Shopping group
    public Guid ShoppingGroupId { get; set; }
    [JsonIgnore]
    public virtual ShoppingGroup ShoppingGroup { get; set; }

    // FK -> ShopProduct
    [JsonIgnore]
    public virtual List<ShopProduct> ShopProducts { get; set; }

    // Response stuff
    [NotMapped]
    public virtual List<ProductForShop> Products { get; set; }
  }
}