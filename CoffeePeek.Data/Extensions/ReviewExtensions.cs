using CoffeePeek.Data.Models.Review;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Extensions;

internal static class ReviewExtensions
{
    public static void ReviewConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>()
            .HasQueryFilter(r => !r.User.IsSoftDeleted)
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<RatingCategory>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}