using CoffeePeek.Moderation.Domain;
using CoffeePeek.Moderation.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Moderation.Persistence.Configuration;

public class ModerationRoasterConfiguration : IEntityTypeConfiguration<ModerationRoaster>
{
    public void Configure(EntityTypeBuilder<ModerationRoaster> entity)
    {
        entity.UsePropertyAccessMode(PropertyAccessMode.Field);

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).HasMaxLength(BusinessConstants.MaxRoasterNameLength);
        entity.Property(e => e.About).HasMaxLength(BusinessConstants.MaxRoasterAboutLength);
        entity.Property(e => e.RejectedReason).HasMaxLength(200);
        entity.HasIndex(e => e.UserId);
        entity.HasIndex(e => e.ModerationStatus);

        entity.OwnsOne(e => e.Contact, contact =>
        {
            contact.Property(c => c.InstagramLink).HasMaxLength(BusinessConstants.MaxShopContactInstagramLinkLength);
            contact.Property(c => c.SiteLink).HasMaxLength(BusinessConstants.MaxShopContactSiteLinkLength);
        });

        entity.OwnsOne(e => e.Location, location => { location.HasIndex(l => new { l.Latitude, l.Longitude }); });
    }
}
