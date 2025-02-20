using CoffeePeek.Data.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Extensions;

internal static class AddressExtensions
{
    public static void AddressConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<City>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Country>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Street>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}