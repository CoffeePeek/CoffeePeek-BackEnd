using CoffeePeek.Data.Entities.Users;

namespace CoffeePeek.Data.Entities.Shop;

public class ShopPhoto : BaseEntity
{
    public string Url { get; set; }
    public int ShopId { get; set; }
    /// <summary>
    /// Id creator photo 
    /// </summary>
    public int UserId { get; set; }
    
    public virtual Shop Shop { get; set; }
    public virtual User User { get; set; }
}