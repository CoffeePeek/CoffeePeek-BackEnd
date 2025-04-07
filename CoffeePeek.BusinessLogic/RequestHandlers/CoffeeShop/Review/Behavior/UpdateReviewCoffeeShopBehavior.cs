using System.Collections.ObjectModel;
using CoffeePeek.Contract.Requests.CoffeeShop.Review;
using CoffeePeek.Contract.Response.CoffeeShop.Review;
using CoffeePeek.Shared.Models.PhotoUpload;
using MassTransit;
using MediatR;
using Microsoft.VisualBasic;

namespace CoffeePeek.BusinessLogic.RequestHandlers.CoffeeShop.Review.Behavior;

internal class UpdateReviewCoffeeShopBehavior(IPublishEndpoint publishEndpoint)
    : IPipelineBehavior<UpdateReviewCoffeeShopRequest, Contract.Response.Response<UpdateReviewCoffeeShopResponse>>
{
    public async Task<Contract.Response.Response<UpdateReviewCoffeeShopResponse>> Handle(UpdateReviewCoffeeShopRequest request,
        RequestHandlerDelegate<Contract.Response.Response<UpdateReviewCoffeeShopResponse>> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.Photos is { Count: > 0 } && response.Success)
        {
            var photosInBytes = new List<byte[]>();
            foreach (var file in request.Photos.Where(file => file.Length > 0))
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms, cancellationToken);
                photosInBytes.Add(ms.ToArray());
            }
            
            await publishEndpoint.Publish<IPhotoUploadRequested>(new
            {
                UserId = request.UserId,
                ShopId = request.ReviewShopId,
                Photos = photosInBytes
            }, cancellationToken);
        }
        
        return response;
    }
}