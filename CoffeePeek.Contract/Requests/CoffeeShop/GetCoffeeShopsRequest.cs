using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;

namespace CoffeePeek.Contract.Requests.CoffeeShop;

public class GetCoffeeShopsRequest : IRequest<Response.Response<GetCoffeeShopsResponse>>
{
    public int CityId { get; }
    public int PageNumber { get; }
    public int PageSize { get; }

    public GetCoffeeShopsRequest(int cityId, int pageNumber, int pageSize)
    {
        CityId = cityId;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}