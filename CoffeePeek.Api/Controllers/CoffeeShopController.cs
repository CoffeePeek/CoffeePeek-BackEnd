using CoffeePeek.BusinessLogic.Services.Auth;
using CoffeePeek.Contract.Constants;
using CoffeePeek.Contract.Requests.CoffeeShop;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoffeeShopController(IMediator mediator, IUserContextService userContextService) : Controller
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

    [HttpPost("send-to-review")]
    [Authorize]
    public async Task<Response<SendCoffeeShopToReviewResponse>> SendCoffeeShopToReview(
        [FromBody] SendCoffeeShopToReviewRequest request)
    {
        if (!userContextService.TryGetUserId(out var userId))
        {
            return Contract.Response.Response.ErrorResponse<Response<SendCoffeeShopToReviewResponse>>(
                "User ID not found or invalid.");
        }
    
        request.UserId = userId;
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
}