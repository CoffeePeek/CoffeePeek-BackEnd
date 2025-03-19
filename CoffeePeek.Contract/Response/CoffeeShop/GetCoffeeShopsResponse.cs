using System.Text.Json.Serialization;
using CoffeePeek.Contract.Dtos.CoffeeShop;

namespace CoffeePeek.Contract.Response.CoffeeShop;

public class GetCoffeeShopsResponse
{
    [JsonPropertyName("content")]
    public CoffeeShopDto[] CoffeeShopDtos { get; set; }
    [JsonPropertyName("page")]
    public int CurrentPage { get; set; }
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }
    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}