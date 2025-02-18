using CoffeePeek.Data.Models.Review;
using CoffeePeek.Data.Models.Shop;
using CoffeePeek.Data.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Databases;

public class CoffeePeekDbContext : IdentityDbContext<User, IdentityRoleEntity, int>
{
    public virtual DbSet<Shop> Shops { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public CoffeePeekDbContext(DbContextOptions<CoffeePeekDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasQueryFilter(author => !author.IsSoftDeleted);
        
        modelBuilder.Entity<Review>().HasQueryFilter(r => !r.User.IsSoftDeleted);
        
        base.OnModelCreating(modelBuilder);
    }
}