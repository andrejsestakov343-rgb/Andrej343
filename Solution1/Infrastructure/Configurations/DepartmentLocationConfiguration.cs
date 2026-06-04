using Domain.Departments;
using Domain.Departments.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class DepartmentLocationConfigurations : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_location");
        builder.HasKey(dl => new { dl.DepartmentId, dl.LocationId }).HasName("id_department_location");
        builder.Property(dl => dl.DepartmentId).HasColumnName("id_department").HasConversion(a => a.Value, a => DepartmentId.Create(a));
        builder.Property(dl => dl.LocationId).HasColumnName("id_location");
        builder.HasOne(dl => dl.Department).WithMany(d => d.DepartmentLocations).HasForeignKey(dl => dl.DepartmentId);
        builder.HasOne(dl => dl.Location).WithMany(l => l.DepartmentLocations).HasForeignKey(dl => dl.LocationId);
    }
}
