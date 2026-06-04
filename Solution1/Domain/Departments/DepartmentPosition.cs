using Domain.Departments.ValueObjects;
using Domain.PositionContext;

namespace Domain.Departments;

public class DepartmentPosition
{
    public DepartmentPosition(Department department, Position position)
    {
        Department = department;
        Position = position;
        DepartmentId = department.Id;
        PositionId = position.Id;

    }
    public DepartmentPosition()
    {
    }
    public Department Department { get; init; }

    public Position Position { get; init; }
    public DepartmentId DepartmentId { get; }
    public Guid PositionId  { get;}
    public DateTime AssignedAt { get; init; } = DateTime.UtcNow;
}



















