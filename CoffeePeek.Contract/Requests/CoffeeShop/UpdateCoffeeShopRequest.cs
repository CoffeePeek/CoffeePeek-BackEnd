using System.Text.Json.Serialization;
using CoffeePeek.Contract.Dtos.Address;
using CoffeePeek.Contract.Dtos.Schedule;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Contract.Requests.CoffeeShop;

public class UpdateCoffeeShopRequest : IRequest<Response<UpdateCoffeeShopResponse>>
{
    [JsonIgnore] public int UserId { get; set; }
    [FromBody] public int ShopId { get; set; }
    [FromBody] public string? Description { get; set; }
    [FromBody] public AddressDto? Address { get; set; }
    [FromBody] public List<IFormFile>? ShopPhotos { get; set; }
    [FromBody] public List<ScheduleDto>? Schedules { get; set; }
    [FromBody] public List<ScheduleExceptionDto>? ScheduleExceptions { get; set; }
}