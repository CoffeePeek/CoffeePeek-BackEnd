using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Entities.Users;

namespace CoffeePeek.Data.Repositories;

public class UserRepository(CoffeePeekDbContext context) : Repository<User>(context)
{
}