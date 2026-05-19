using System.Security.Cryptography.X509Certificates;
using Domain.Departments;
using Domain.Departments.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class DepartmentConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("department");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").IsRequired().HasConversion(x => x.Value, x => DepartmentId.Create(x));
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(255).IsRequired().HasConversion(x => x.Value, x => DepartmentName.Create(x));
        builder.Property(x => x.ParentId).HasColumnName("parent_id").IsRequired().HasConversion(x => x.Value, x => DepartmentId.Create(x));
        builder.Property(x => x.Identifier).HasColumnName("identifier").HasMaxLength(255).IsRequired().HasConversion(x => x.Value, x => DepartmentIdentifier.Create(x));
        builder.Property(x => x.Path).HasColumnName("path").HasMaxLength(255).IsRequired().HasConversion(x => x.Value, x => DepartmentPath.Create(x));
        builder.Property(x => x.Depth).HasColumnName("depth").HasMaxLength(255).IsRequired().HasConversion(x => x.Value, x => DepartmentDepth.Create(x));

        builder.ComplexProperty(x => x.LifeTime, complexPropertyBuilder =>
   {
       complexPropertyBuilder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();

       complexPropertyBuilder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

       complexPropertyBuilder.Property(p => p.IsArchived).HasColumnName("is_archived").IsRequired();
   });
        builder.HasMany(a => a.DepartmentPositions).WithOne();
        builder.HasMany(b => b.DepartmentLocations).WithOne();
    }

}