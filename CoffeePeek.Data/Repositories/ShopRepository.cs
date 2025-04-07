using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Entities.Shop;
using CoffeePeek.Data.Models.Shop;

namespace CoffeePeek.Data.Repositories;

public class ShopRepository(CoffeePeekDbContext context) : Repository<Shop>(context);