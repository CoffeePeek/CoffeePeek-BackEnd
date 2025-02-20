using CoffeePeek.Data.Models.Users;

namespace CoffeePeek.Data.Models.Review;

public class Review : BaseModel
{
    public string Header { get; set; }
    public string Comment { get; set; }
    public int UserId { get; set; }
    public int ShopId { get; set; }
    public DateTime ReviewDate { get; set; }
    
    public ICollection<ReviewRatingCategory> ReviewRatingCategories { get; set; }

    public virtual User User { get; set; }
    public virtual Shop.Shop Shop { get; set; }
}