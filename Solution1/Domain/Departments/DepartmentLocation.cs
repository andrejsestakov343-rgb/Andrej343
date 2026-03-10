using Domain.Departments.ValueObjects;
using Domain.Locations;
using Domain.Locations.ValueObjects;

namespace Domain.Departments
{
    public class DepartmentLocation
    {
        public DepartmentId DepartmentId { get; private set; } = null!;
        public Department Department { get; private set; } = null!;
        public LocationId LocationId { get; private set; } = null!;
        public Location Location { get; private set; } = null!;
        public DateTime AssignedAt { get; private set; }

        public DepartmentLocation(Department department, Location location)
        {
            DepartmentId = department?.Id ?? throw new ArgumentNullException(nameof(department));
            Department = department ?? throw new ArgumentNullException(nameof(department));
            LocationId = location?.Id ?? throw new ArgumentNullException(nameof(location));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            AssignedAt = DateTime.UtcNow;
        }
    }
}