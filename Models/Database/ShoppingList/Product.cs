using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HomeAutomation.Helpers;

namespace HomeAutomation.Models.Database.ShoppingList
{
  public class Product
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    [JsonIgnore]
    public virtual List<ShopProduct> ShopProducts { get; set; }
  }
}