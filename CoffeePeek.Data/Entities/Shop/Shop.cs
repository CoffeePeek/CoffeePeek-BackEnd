using CoffeePeek.Data.Enums.Shop;
using CoffeePeek.Data.Models.Schedules;
using CoffeePeek.Data.Models.Shop;

namespace CoffeePeek.Data.Entities.Shop;

public class Shop : BaseEntity
{
    public Shop()
    {
        ShopPhotos = new HashSet<ShopPhoto>();
        Schedules = new HashSet<Schedule>();
        ScheduleExceptions = new HashSet<ScheduleException>();
        Reviews = new HashSet<Models.Review.Review>();
    }
    
    public string Name { get; set; }
    public int AddressId { get; set; }
    public int? ShopContactId { get; set; }

    public Entities.Address.Address Address { get; set; }
    public ShopStatus Status { get; set; }
    public virtual ShopContacts ShopContacts { get; set; }

    public ICollection<ShopPhoto> ShopPhotos { get; set; }
    public ICollection<Schedule> Schedules { get; set; }
    public ICollection<ScheduleException> ScheduleExceptions { get; set; }
    public ICollection<Models.Review.Review> Reviews { get; set; }
}