using CoffeePeek.BusinessLogic.Models;
using CoffeePeek.Contract.Requests.CoffeeShop.Review;
using CoffeePeek.Contract.Response;
using CoffeePeek.Data;
using CoffeePeek.Data.Models.Shop;
using MediatR;

namespace CoffeePeek.BusinessLogic.RequestHandlers.CoffeeShop.Review;

public class UpdateReviewCoffeeShopStatusRequestHandler(IRepository<ReviewShop> reviewRepository)
    : IRequestHandler<UpdateReviewCoffeeShopStatusRequest, Response>
{
    public async Task<Response> Handle(UpdateReviewCoffeeShopStatusRequest request, CancellationToken cancellationToken)
    {
        var reviewShop = await reviewRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (reviewShop == null)
        {
            return Response.ErrorResponse<Response>("CoffeeShop not found");
        }
        
        var model = new ReviewShopModel(reviewShop);
        
        model.UpdateStatus(request.ReviewStatus);

        await reviewRepository.SaveChangesAsync(cancellationToken);
        
        return Response.SuccessResponse<Response>();
    }
}