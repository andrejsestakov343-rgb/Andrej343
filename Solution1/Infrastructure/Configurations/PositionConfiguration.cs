using System.Security.Cryptography.X509Certificates;
using Domain.Departments.ValueObjects;
using Domain.Positions;
using Domain.Positions.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class PositionConfigurations : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("position");
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(255).IsRequired().HasConversion(x => x.Name, x => PositionName.Create(x));
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Id).HasColumnName("id").IsRequired().HasConversion(x => x.Value, x => new PositionId(x));
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(x => x.LifeTime, complexPropertyBuilder =>
   {
        complexPropertyBuilder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();

        complexPropertyBuilder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        complexPropertyBuilder.Property(p => p.IsArchived).HasColumnName("is_archived").IsRequired();
   });

    }
}

