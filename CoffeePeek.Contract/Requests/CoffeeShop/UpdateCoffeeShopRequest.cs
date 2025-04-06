using System.Text.Json.Serialization;
using CoffeePeek.Contract.Dtos.Address;
using CoffeePeek.Contract.Dtos.Schedule;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CoffeePeek.Contract.Requests.CoffeeShop;

public class UpdateCoffeeShopRequest : IRequest<Response<UpdateCoffeeShopResponse>>
{
    [JsonIgnore] public int UserId { get; set; }
    public int ShopId { get; set; }
    public string? Description { get; set; }
    public AddressDto? Address { get; set; }
    public List<IFormFile>? ShopPhotos { get; set; }
    public List<ScheduleDto>? Schedules { get; set; }
    public List<ScheduleExceptionDto>? ScheduleExceptions { get; set; }
}