using CoffeePeek.Contract.Requests.CoffeeShop.Review;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using CoffeePeek.Data;
using CoffeePeek.Data.Enums.Shop;
using CoffeePeek.Data.Models.Address;
using CoffeePeek.Data.Models.Shop;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.BusinessLogic.RequestHandlers.CoffeeShop;

public class SendCoffeeShopToReviewRequestHandler(
    IMapper mapper, 
    IRepository<ReviewShop> reviewShopRepository)
    : IRequestHandler<SendCoffeeShopToReviewRequest, Response<SendCoffeeShopToReviewResponse>>
{
    public async Task<Response<SendCoffeeShopToReviewResponse>> Handle(SendCoffeeShopToReviewRequest request,
        CancellationToken cancellationToken)
    {
        var shopInReviewExists = await reviewShopRepository
            .FindBy(x => x.Name == request.Name && x.Shop.Name == request.Name)
            .AnyAsync(cancellationToken);

        if (shopInReviewExists)
        {
            return Response.ErrorResponse<Response<SendCoffeeShopToReviewResponse>>("Is already exists.");
        }

        var reviewShop = mapper.Map<ReviewShop>(request);

        reviewShop.ReviewStatus = ReviewStatus.Pending;
        
        reviewShopRepository.Add(reviewShop);
        
        await reviewShopRepository.SaveChangesAsync(cancellationToken);
        
        return Response.SuccessResponse<Response<SendCoffeeShopToReviewResponse>>("CoffeeShop added to review.");
    }
}