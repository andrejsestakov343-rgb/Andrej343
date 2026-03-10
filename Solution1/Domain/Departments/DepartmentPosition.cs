using Domain.Departments;
using Domain.Departments.ValueObjects;
using Domain.Positions;
using Domain.Positions.ValueObjects;

public class DepartmentPosition
{
    public DepartmentId DepartmentId { get; private set; } = null!;
    public Department Department { get; private set; } = null!;
    public PositionId PositionId { get; private set; } = null!;
    public Position Position { get; private set; } = null!;
    public DateTime AssignedAt { get; private set; }

    public DepartmentPosition(Department department, Position position)
    {
        DepartmentId = department?.Id ?? throw new ArgumentNullException(nameof(department));
        Department = department ?? throw new ArgumentNullException(nameof(department));
        PositionId = position?.Id ?? throw new ArgumentNullException(nameof(position));
        Position = position ?? throw new ArgumentNullException(nameof(position));
        AssignedAt = DateTime.UtcNow;
    }
}
















