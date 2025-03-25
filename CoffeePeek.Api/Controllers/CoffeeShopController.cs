using CoffeePeek.Contract.Constants;
using CoffeePeek.Contract.Requests.CoffeeShop;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using CoffeePeek.Infrastructure.Services.Auth.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CoffeePeek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoffeeShopController(IMediator mediator, IUserContextService userContextService) : Controller
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

    [HttpPost("send-to-review")]
    [Authorize]
    public async Task<Response<SendCoffeeShopToReviewResponse>> SendCoffeeShopToReview(SendCoffeeShopToReviewRequest request, IFormFile file)
    {
        if (!userContextService.TryGetUserId(out var userId))
        {
            return Contract.Response.Response.ErrorResponse<Response<SendCoffeeShopToReviewResponse>>(
                "User ID not found or invalid.");
        }

        request.UserId = userId;
        request.ShopPhotos.Add(file);
        
        return await mediator.Send(request);
    }
    
    [HttpGet("in-review")]
    [Authorize]
    public async Task<Response<GetCoffeeShopsInReviewByIdResponse>> GetCoffeeShopsInReviewById()
    {
        if (!userContextService.TryGetUserId(out var userId))
        {
            return Contract.Response.Response.ErrorResponse<Response<GetCoffeeShopsInReviewByIdResponse>>(
                "User ID not found or invalid.");
        }

        var request = new GetCoffeeShopsInReviewByIdRequest(userId);
        
        return await mediator.Send(request);
    }

    private void AddPaginationHeaders(GetCoffeeShopsResponse data)
    {
        Response.Headers.TryAdd("X-Total-Count", data.TotalItems.ToString());
        Response.Headers.TryAdd("X-Total-Pages", data.TotalPages.ToString());
        Response.Headers.TryAdd("X-Current-Page", data.CurrentPage.ToString());
        Response.Headers.TryAdd("X-Page-Size", data.PageSize.ToString());
    }
}