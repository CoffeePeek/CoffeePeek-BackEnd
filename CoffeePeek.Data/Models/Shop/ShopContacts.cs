namespace CoffeePeek.Data.Models.Shop;

public class ShopContacts : BaseModel
{
    public int ShopId { get; set; }
    public string PhoneNumber { get; set; }
    public string InstagramLink { get; set; }
    
    public virtual Shop Shop { get; set; }
}