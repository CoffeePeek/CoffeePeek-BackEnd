using CoffeePeek.Contract.Constants;
using CoffeePeek.Contract.Requests.CoffeeShop;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CoffeePeek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoffeeShopController(IMediator mediator) : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(Response<GetCoffeeShopsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponseHeader(200, "X-Total-Count", "integer", "Total number of items")]
    [SwaggerResponseHeader(200, "X-Total-Pages", "integer", "Total number of pages")]
    [SwaggerResponseHeader(200, "X-Current-Page", "integer", "Current page number")]
    [SwaggerResponseHeader(200, "X-Page-Size", "integer", "Page size")]
    public async Task<Response<GetCoffeeShopsResponse>> GetCoffeeShops(
        [FromQuery] int cityId,
        [FromHeader(Name = "X-Page-Number")] int pageNumber = 1,
        [FromHeader(Name = "X-Page-Size")] int pageSize = 10)
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            cityId = BusinessConstants.DefaultUnAuthorizedCityId;
        }

        var response = await mediator.Send(new GetCoffeeShopsRequest(cityId, pageNumber, pageSize));

        AddPaginationHeaders(response.Data);

        return response;
    }

    [HttpPost]
    [Authorize]
    public Task<Response<UpdateCoffeeShopResponse>> UpdateCoffeeShop(UpdateCoffeeShopRequest request)
    {
        return mediator.Send(request);
    }
    
    private void AddPaginationHeaders(GetCoffeeShopsResponse data)
    {
        Response.Headers.TryAdd("X-Total-Count", data.TotalItems.ToString());
        Response.Headers.TryAdd("X-Total-Pages", data.TotalPages.ToString());
        Response.Headers.TryAdd("X-Current-Page", data.CurrentPage.ToString());
        Response.Headers.TryAdd("X-Page-Size", data.PageSize.ToString());
    }
}