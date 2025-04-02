using CoffeePeek.Contract.Dtos.Address;
using CoffeePeek.Contract.Dtos.Contact;
using CoffeePeek.Contract.Dtos.Schedule;
using CoffeePeek.Data.Enums.Shop;
using Newtonsoft.Json;

namespace CoffeePeek.Contract.Dtos.CoffeeShop;

public class ReviewShopDto
{
    public string Name { get; set; }
    public string NotValidatedAddress { get; set; }
    [JsonIgnore] public int? AddressId { get; set; }
    [JsonIgnore] public int? ShopContactId { get; set; }
    [JsonIgnore] public int UserId { get; set; }

    public ReviewStatus ReviewStatus { get; set; }
    public AddressDto Address { get; set; }
    public ShopStatus Status { get; set; }
    public ShopContactDto ShopContact { get; set; }

    public ICollection<string> ShopPhotos { get; set; }
    public ICollection<ScheduleDto> Schedules { get; set; }
    public ICollection<ScheduleExceptionDto> ScheduleExceptions { get; set; }
}