namespace HomeAutomation.Areas.ShoppingList.Models
{
  public class MoveProductRequest
  {
    public int OldShopId { get; set; }
    public int NewShopId { get; set; }
    public int ShopProductId { get; set; }

  }
}