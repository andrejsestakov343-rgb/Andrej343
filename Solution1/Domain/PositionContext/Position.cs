using Domain.Departments;

namespace Domain.PositionContext
{
    public class Position
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Position(Guid id, string name, string? description, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = true;
        }
        
        private Position()
        {
        }
        
        public void ChangeName(string newName)
        {
            if (!IsActive)
                throw new InvalidOperationException("Редактирование неактивных позиций запрещено");

            Name = newName ?? throw new ArgumentNullException(nameof(newName));
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeDescription(string? description)
        {
            if (!IsActive)
                throw new InvalidOperationException("Редактирование неактивных позиций запрещено");

            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Позиция уже неактивна");

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Позиция уже активна");

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public override bool Equals(object? obj) => obj is Position other && Id == other.Id;
        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Position? left, Position? right) =>
            left is not null && right is not null && left.Equals(right);

        public static bool operator !=(Position? left, Position? right) => !(left == right);

        public ICollection<DepartmentPosition> DepartmentPositions { get; } = new List<DepartmentPosition>();
    }
}
