using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Departments;
using Domain.Departments.ValueObjects;

namespace Infrastructure.Configurations;

internal sealed class DepartmentPositionConfigurations : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_position");
        builder.HasKey(dp => new { dp.PositionId, dp.DepartmentId }).HasName("id_department_position");
        builder.Property(dp => dp.PositionId).HasColumnName("id_position");
        builder.Property(dp => dp.DepartmentId).HasColumnName("id_department").HasConversion(b => b.Value, b => DepartmentId.Create(b));
        builder.HasOne(dp=> dp.Position).WithMany(p => p.DepartmentPositions).HasForeignKey(dp => dp.PositionId);
        builder.HasOne(dp => dp.Department).WithMany(d => d.DepartmentPositions).HasForeignKey(dp => dp.DepartmentId);
    }
}
