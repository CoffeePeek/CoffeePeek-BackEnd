namespace CoffeePeek.Data.Models.Shop;

public class ShopPhoto : BaseModel
{
    public string Url { get; set; }
    public int ShopId { get; set; }
    
    public virtual Shop Shop { get; set; }
}