using System;
using CoffeePeek.Data.Models.Review;
using CoffeePeek.Data.Models.Shop;
using CoffeePeek.Data.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Tests.UnitOfWork;

public class InMemoryContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Shop> Shops { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}