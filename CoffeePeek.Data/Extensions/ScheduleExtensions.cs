using CoffeePeek.Data.Models.Schedules;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.Data.Extensions;

internal static class ScheduleExtensions
{
    public static void ScheduleConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schedule>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Schedule>()
            .Property(b => b.IsOpen24Hours)
            .HasDefaultValue(false);
        
        modelBuilder.Entity<ScheduleException>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}