using System.Text.Json.Serialization;
using CoffeePeek.Contract.Dtos.Address;
using CoffeePeek.Contract.Dtos.Schedule;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using CoffeePeek.Contract.Response.CoffeeShop.Review;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Contract.Requests.CoffeeShop.Review;

public class UpdateReviewCoffeeShopRequest : IRequest<Response<UpdateReviewCoffeeShopResponse>>
{
    [JsonIgnore] public int UserId { get; set; }
    [FromBody] public int ReviewShopId { get; set; }
    [FromBody] public string? Description { get; set; }
    [FromBody] public List<IFormFile>? Photos { get; set; }
    [FromBody] public AddressDto? Address { get; set; }
    [FromBody] public List<IFormFile>? ShopPhotos { get; set; }
    [FromBody] public List<ScheduleDto>? Schedules { get; set; }
    [FromBody] public List<ScheduleExceptionDto>? ScheduleExceptions { get; set; }
}