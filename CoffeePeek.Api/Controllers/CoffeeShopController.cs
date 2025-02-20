using CoffeePeek.Contract.Constants;
using CoffeePeek.Contract.Requests.CoffeeShop;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoffeeShopController(IMediator mediator) : Controller
{
    [HttpGet]
    public Task<Response<GetCoffeeShopsResponse>> GetCoffeeShops([FromQuery] int cityId, int pageNumber = 1, int pageSize = 10)
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            cityId = BusinessConstants.DefaultUnAuthorizedCityId;
        }
        
        var request = new GetCoffeeShopsRequest(cityId, pageNumber, pageSize);
        return mediator.Send(request);
    }
}