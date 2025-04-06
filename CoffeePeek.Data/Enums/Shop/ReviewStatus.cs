using System.Text.Json.Serialization;

namespace CoffeePeek.Data.Enums.Shop;

public enum ReviewStatus
{
    [JsonPropertyName("Pending")]
    Pending,

    [JsonPropertyName("Approved")]
    Approved,

    [JsonPropertyName("Rejected")]
    Rejected
}