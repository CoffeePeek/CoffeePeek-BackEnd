using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Shop;

namespace CoffeePeek.Data.Repositories.InReview;

public class ReviewShopsRepository(ReviewCoffeePeekDbContext context) : Repository<ReviewShop>(context);