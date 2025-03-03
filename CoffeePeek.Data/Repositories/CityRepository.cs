using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Address;

namespace CoffeePeek.Data.Repositories;

public class CityRepository(CoffeePeekDbContext context) : Repository<City>(context);