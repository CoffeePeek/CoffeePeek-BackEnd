using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;

namespace CoffeePeek.Contract.Requests.CoffeeShop;

public class GetCoffeeShopsInReviewByIdRequest(int userId) : IRequest<Response<GetCoffeeShopsInReviewByIdResponse>>
{
    public int UserId { get; set; } = userId;
}