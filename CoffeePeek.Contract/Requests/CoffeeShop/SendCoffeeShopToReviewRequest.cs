#nullable enable
using System.Text.Json.Serialization;
using CoffeePeek.Contract.Dtos.Address;
using CoffeePeek.Contract.Dtos.Schedule;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CoffeePeek.Contract.Requests.CoffeeShop;

public class SendCoffeeShopToReviewRequest : IRequest<Response<SendCoffeeShopToReviewResponse>>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public AddressDto Address { get; set; }
    [JsonIgnore]
    public List<IFormFile>? ShopPhotos { get; set; } =  new List<IFormFile>();
    public List<ScheduleDto>? Schedules { get; set; }
    public List<ScheduleExceptionDto>? ScheduleExceptions { get; set; }
    public int UserId { get; set; }
}