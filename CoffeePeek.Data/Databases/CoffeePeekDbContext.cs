using CoffeePeek.Data.Models.Address;
using CoffeePeek.Data.Models.Review;
using CoffeePeek.Data.Models.Schedules;
using CoffeePeek.Data.Models.Shop;
using CoffeePeek.Data.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Databases;

public class CoffeePeekDbContext : IdentityDbContext<User, IdentityRoleEntity, int>
{
    public virtual DbSet<Shop> Shops { get; set; }
    public virtual DbSet<ShopContacts> ShopContacts { get; set; }
    
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<RatingCategory> RatingCategories { get; set; }
    public virtual DbSet<ReviewRatingCategory> ReviewRatingCategories { get; set; }
    
    public virtual DbSet<Schedule> Schedules { get; set; }
    public virtual DbSet<ScheduleException> ScheduleExceptions { get; set; }
    
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Street> Streets { get; set; }
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