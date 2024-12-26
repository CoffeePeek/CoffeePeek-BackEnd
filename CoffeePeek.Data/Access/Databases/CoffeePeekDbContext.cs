using CoffeePeek.Data.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Access.Databases;

public class CoffeePeekDbContext : DbContext
{
    public CoffeePeekDbContext(DbContextOptionsBuilder<CoffeePeekDbContext> options)
    {
        
    }

    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}