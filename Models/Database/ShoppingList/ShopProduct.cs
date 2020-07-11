namespace HomeAutomation.Models.Database.ShoppingList
{
  public class ShopProduct
  {
    public int Id { get; set; }
    public int ShopId { get; set; }
    public int ProductId { get; set; }

    public string Amount { get; set; }
    public bool Bought { get; set; }

    public Shop Shop { get; set; }
    public Product Product { get; set; }
  }
}