using System.Text.Json.Serialization;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;

namespace CoffeePeek.Contract.Requests.CoffeeShop.Review;

public class SendCoffeeShopToReviewRequest : IRequest<Response<SendCoffeeShopToReviewResponse>>
{
    public string Name { get; set; }
    public string? Address { get; set; }
    [JsonIgnore]
    public int UserId { get; set; }
}