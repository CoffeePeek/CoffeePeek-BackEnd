namespace CoffeePeek.Data.Models.Address;

public class City : BaseModel
{
    public string Name { get; set; }
    public int CountryId { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    
    public Country Country { get; set; }
}