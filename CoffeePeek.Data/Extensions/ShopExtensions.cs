using CoffeePeek.Data.Models.Shop;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Extensions;

internal static class ShopExtensions
{
    public static void ShopConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shop>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<ShopContacts>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<ShopPhoto>()
            .HasIndex(x => x.Url);
        modelBuilder.Entity<ShopPhoto>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}