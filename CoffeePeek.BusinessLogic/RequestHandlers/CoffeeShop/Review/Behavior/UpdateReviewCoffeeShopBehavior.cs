using CoffeePeek.Contract.Requests.CoffeeShop.Review;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop.Review;
using MediatR;

namespace CoffeePeek.BusinessLogic.RequestHandlers.CoffeeShop.Review.Behavior;

internal class UpdateReviewCoffeeShopBehavior 
    : IPipelineBehavior<UpdateReviewCoffeeShopRequest, Response<UpdateReviewCoffeeShopResponse>>
{
    public async Task<Response<UpdateReviewCoffeeShopResponse>> Handle(UpdateReviewCoffeeShopRequest request,
        RequestHandlerDelegate<Response<UpdateReviewCoffeeShopResponse>> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.Photos is { Count: > 0 } && response.Success)
        {
            //sending to service
        }
        
        return response;
    }
}