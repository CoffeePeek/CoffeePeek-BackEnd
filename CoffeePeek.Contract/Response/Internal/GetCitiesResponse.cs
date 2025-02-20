using CoffeePeek.Contract.Dtos.Internal;

namespace CoffeePeek.Contract.Response.Internal;

public class GetCitiesResponse
{
    public GetCitiesResponse(CityDto[] city)
    {
        City = city;
    }

    public CityDto[] City { get; set; }
}