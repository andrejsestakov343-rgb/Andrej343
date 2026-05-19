using System.Security.Cryptography.X509Certificates;
using Domain.Locations;
using Domain.Locations.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class LocationConfigurations : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("location");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").HasConversion(x => x.Value, x => LocationId.Create(x));
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(255).IsRequired().HasConversion(x => x.Value, x => LocationName.Create(x));
        builder.Property(x => x.Address).HasColumnName("address").HasMaxLength(255).IsRequired().HasConversion(x => x.Value, x => LocationAddress.Create(x));
        builder.Property(x => x.TimeZone).HasColumnName("iana_time_zone").HasMaxLength(255).IsRequired().HasConversion(x => x.Value, x => IanaTimeZone.Create(x));

        builder.ComplexProperty(x => x.LifeTime, complexPropertyBuilder =>
   {
        complexPropertyBuilder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();

        complexPropertyBuilder .Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        complexPropertyBuilder.Property(p => p.IsArchived).HasColumnName("is_archived").IsRequired();
    });

    }
}
