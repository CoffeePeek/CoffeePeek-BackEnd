using CoffeePeek.Contract.Dtos.CoffeeShop;

namespace CoffeePeek.Contract.Response.CoffeeShop;

public class GetCoffeeShopsInReviewByIdResponse
{
    public GetCoffeeShopsInReviewByIdResponse(ReviewShopDto[] reviewShops)
    {
        ReviewShops = reviewShops;
    }

    public ReviewShopDto[] ReviewShops { get; }
}