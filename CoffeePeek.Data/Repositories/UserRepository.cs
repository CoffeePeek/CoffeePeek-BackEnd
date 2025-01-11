using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Users;

namespace CoffeePeek.Data.Repositories;

public class UserRepository : Repository<User>
{
    private readonly CoffeePeekDbContext _context;
    
    public UserRepository(CoffeePeekDbContext context) : base(context)
    {
        _context = context;
    }
}