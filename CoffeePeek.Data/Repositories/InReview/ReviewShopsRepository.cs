using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Entities.Shop;
using CoffeePeek.Data.Models.Shop;

namespace CoffeePeek.Data.Repositories.InReview;

public class ReviewShopsRepository(CoffeePeekDbContext context) : Repository<ReviewShop>(context);