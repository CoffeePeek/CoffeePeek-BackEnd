using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Users;

namespace CoffeePeek.Data.Repositories;

public class UserRepository(CoffeePeekDbContext context) : Repository<User>(context)
{
}