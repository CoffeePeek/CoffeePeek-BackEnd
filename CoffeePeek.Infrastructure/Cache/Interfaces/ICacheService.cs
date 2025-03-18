using CoffeePeek.Contract.Dtos.Internal;

namespace CoffeePeek.Infrastructure.Cache.Interfaces;

public interface ICacheService
{
    Task<ICollection<CityDto>> GetCities();
}