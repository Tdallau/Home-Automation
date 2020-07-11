namespace HomeAutomation.Areas.ShoppingList.Models
{
  public class ProductForShop
  {
    public int Id { get; set; }
    public int ShopProductId { get; set; }
    public string Name { get; set; }
    public string Amount { get; set; }
    public bool Bought { get; set; }
  }
}