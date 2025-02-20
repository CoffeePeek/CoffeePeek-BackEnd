namespace CoffeePeek.Data.Models.Address;

public class Street : BaseModel
{
    public string Name { get; set; }
    public int CityId { get; set; }

    public City City { get; set; }
}