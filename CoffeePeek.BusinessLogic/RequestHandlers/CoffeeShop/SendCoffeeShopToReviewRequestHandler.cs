using System.Net.Http.Json;
using CoffeePeek.Contract.Requests.CoffeeShop;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using CoffeePeek.Data;
using CoffeePeek.Data.Enums.Shop;
using CoffeePeek.Data.Models.Shop;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            .FindBy(x => x.Name == request.Name)
            .AnyAsync(cancellationToken);

        if (shopInReviewExists)
        {
            return Response.ErrorResponse<Response<SendCoffeeShopToReviewResponse>>("Is already exists.");
        }

        var reviewShop = mapper.Map<ReviewShop>(request);

        reviewShop.ReviewStatus = ReviewStatus.Pending;
        
        reviewShopRepository.Add(reviewShop);
        
        await reviewShopRepository.SaveChangesAsync(cancellationToken);
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7220");

        var sendRequest = new SendPhotoRequest(reviewShop.Id, request.ShopPhotos.First());

        var httpRequest = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(sendRequest)
        };
        
        var response = await httpClient.SendAsync(httpRequest, cancellationToken);
        
        return Response.SuccessResponse<Response<SendCoffeeShopToReviewResponse>>("CoffeeShop added to review.");
    }
    
    private class SendPhotoRequest(int photoId, IFormFile file)
    {
        public int PhotoId { get; } = photoId;
        public IFormFile File { get; } = file;
    }
}