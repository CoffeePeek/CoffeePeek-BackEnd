using CoffeePeek.Contract.Dtos.CoffeeShop;

namespace CoffeePeek.Contract.Response.CoffeeShop;

public class GetCoffeeShopsInReviewByIdResponse
{
    public ReviewShopDto[] ReviewShops { get; set; }
}