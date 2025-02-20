using CoffeePeek.Contract.Dtos.Address;
using CoffeePeek.Contract.Dtos.Schedule;

namespace CoffeePeek.Contract.Dtos.CoffeeShop;

public class CoffeeShopDto 
{
    public int CoffeeShopId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AddressDto Address { get; set; }
    public List<ScheduleDto> Schedules { get; set; }
    public List<ScheduleExceptionDto> ScheduleExceptions { get; set; }
}