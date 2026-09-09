using CoffeePeek.Shops.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePeek.Shops.Persistance.Configuration;

public class RoasterPhotoConfiguration : IEntityTypeConfiguration<RoasterPhoto>
{
    public void Configure(EntityTypeBuilder<RoasterPhoto> builder)
    {
        builder.Property(p => p.SortIndex)
            .IsRequired()
            .HasDefaultValue(0);

        // RoasterId is a shadow FK on RoasterPhotos
        builder.HasIndex("RoasterId", nameof(RoasterPhoto.SortIndex))
            .HasDatabaseName("IX_RoasterPhotos_RoasterId_SortIndex");
    }
}
