using CoffeePeek.Data.Models.Shop;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Databases;

public class ReviewCoffeePeekDbContext : DbContext
{
    public virtual DbSet<ReviewShop> ReviewShops { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReviewShop>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        base.OnModelCreating(modelBuilder);
    }
}