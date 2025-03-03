#nullable enable
using System.Text.Json.Serialization;
using CoffeePeek.Contract.Dtos.Address;
using CoffeePeek.Contract.Dtos.Photos;
using CoffeePeek.Contract.Dtos.Schedule;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;

namespace CoffeePeek.Contract.Requests.CoffeeShop;

public class SendCoffeeShopToReviewRequest : IRequest<Response<SendCoffeeShopToReviewResponse>>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public AddressDto Address { get; set; }
    public List<ShopPhotoDtos>? ShopPhotos { get; set; }
    public List<ScheduleDto>? Schedules { get; set; }
    public List<ScheduleExceptionDto>? ScheduleExceptions { get; set; }
    [JsonIgnore] public int UserId { get; set; }
}