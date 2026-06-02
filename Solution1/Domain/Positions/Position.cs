using Domain.Departments;
using Domain.Positions.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Positions
{
    public class Position
    {
        public PositionId Id { get; } = null!;
        public PositionName Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public EntityLifeTime LifeTime { get; private set;} = null!;

        public Position(PositionId id, PositionName name, string? description, EntityLifeTime lifeTime)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            LifeTime = lifeTime;
        }
        private Position()
        {
        }
        public void ChangeName(PositionName newName)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Name = newName ?? throw new ArgumentNullException(nameof(newName));
            LifeTime = LifeTime.Update();
        }

        public void ChangeDescription(string? description)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Description = description;
            LifeTime = LifeTime.Update();
        }

        public void Archive()
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            LifeTime = LifeTime with { UpdatedAt = DateTimeOffset.MinValue };
        }


        public void Restore()
        {
            if (!LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование неархивированных запрещено");

            LifeTime = LifeTime.Update();
        }
        public override bool Equals(object? obj) => obj is Position other && Id == other.Id;
        public override int GetHashCode() => Id.Value.GetHashCode();

        public static bool operator ==(Position? left, Position? right) =>
            left is not null && right is not null && left.Equals(right);

        public static bool operator !=(Position? left, Position? right) => !(left == right);
        public ICollection<DepartmentPosition> DepartmentPositions { get; } = new List<DepartmentPosition>();
    }
}
