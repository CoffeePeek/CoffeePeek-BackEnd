using GeoCoordinatePortable;

namespace CoffeePeek.Data.Models.Shop;

public class Shop : BaseModel
{
    public string Name { get; set; }
    public GeoCoordinate Location { get; set; }
}