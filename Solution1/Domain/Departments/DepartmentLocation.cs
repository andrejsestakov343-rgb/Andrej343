using Domain.Departments.ValueObjects;
using Domain.LocationContext;

namespace Domain.Departments
{
    public class DepartmentLocation
    {
        public DepartmentLocation(Department department, Location location)
        {
            Department = department;
            Location = location;
            DepartmentId = department.Id;
            LocationId = location.Id;
        }
        public DepartmentLocation()
        {
            
        }
        public DepartmentId DepartmentId { get; private set; } = null!;
        public Department Department { get; private set; } = null!;
        public Guid LocationId { get; private set; }
        public Location Location { get; private set; } = null!;
        public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;
    }
}