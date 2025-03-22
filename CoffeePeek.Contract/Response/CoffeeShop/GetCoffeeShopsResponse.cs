using System.Text.Json.Serialization;
using CoffeePeek.Contract.Dtos.CoffeeShop;

namespace CoffeePeek.Contract.Response.CoffeeShop;

public class GetCoffeeShopsResponse
{
    [JsonPropertyName("content")]
    public CoffeeShopDto[] CoffeeShopDtos { get; set; }
    [JsonIgnore]
    public int CurrentPage { get; set; }
    [JsonIgnore]
    public int PageSize { get; set; }
    [JsonIgnore]
    public int TotalItems { get; set; }
    [JsonIgnore]
    public int TotalPages { get; set; }
}