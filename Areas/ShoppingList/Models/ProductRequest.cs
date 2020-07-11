namespace HomeAutomation.Areas.ShoppingList.Models
{
  public class ProductRequest
  {
    public int ShopId { get; set; }
    public int ShopProductId { get; set; }
    public string Name { get; set; }
    public string Amount { get; set; }
  }
}