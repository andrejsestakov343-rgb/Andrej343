using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Domain.PositionContext;
using Domain.LocationContext;
using Domain.Departments;
namespace Infrastructure;

public class DirectoryDbContext(IOptions<DatabaseOptions> options) : DbContext
{
    private readonly DatabaseOptions _options = options.Value;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_options.GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DirectoryDbContext).Assembly);
    }

    public DbSet<Position> Positions { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentPosition> DepartmentPositions { get; set; }
    public DbSet<DepartmentLocation> DepartmentLocations { get; set; }
}
