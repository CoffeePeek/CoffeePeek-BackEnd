using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Users;

namespace CoffeePeek.Data.Repositories;

public class RefreshTokenRepository(CoffeePeekDbContext context) : Repository<RefreshToken>(context)
{
}