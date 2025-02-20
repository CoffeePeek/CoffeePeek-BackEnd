using CoffeePeek.Contract.Enums;
using CoffeePeek.Data.Models.Schedules;

namespace CoffeePeek.Data.Models.Shop;

public class Shop : BaseModel
{
    public Shop()
    {
        ShopPhotos = new HashSet<ShopPhoto>();
        Schedules = new HashSet<Schedule>();
        ScheduleExceptions = new HashSet<ScheduleException>();
        Reviews = new HashSet<Review.Review>();
    }
    
    public string Name { get; set; }
    public int AddressId { get; set; }
    public int? ShopContactId { get; set; }

    public Address.Address Address { get; set; }
    public ShopStatus Status { get; set; }
    public virtual ShopContacts ShopContacts { get; set; }

    public ICollection<ShopPhoto> ShopPhotos { get; set; }
    public ICollection<Schedule> Schedules { get; set; }
    public ICollection<ScheduleException> ScheduleExceptions { get; set; }
    public ICollection<Review.Review> Reviews { get; set; }
}