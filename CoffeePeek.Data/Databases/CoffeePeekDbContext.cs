using CoffeePeek.Data.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Databases;

public class CoffeePeekDbContext : DbContext
{
    public CoffeePeekDbContext(DbContextOptions<CoffeePeekDbContext> options) : base(options)
    {
        
    }

    public virtual DbSet<User> Users { get; set; }
}