using Domain.Positions.ValueObjects;

namespace Domain.Positions
{
    public class Position(
        PositionId id,
        PositionName name,
        string? description,
        EntityLifeTime lifeTime
    )
    {
        public PositionId Id { get; } = id;
        public PositionName Name { get; private set; } = name;
        public string? Description { get; private set; } = description;
        public EntityLifeTime LifeTime { get; } = lifeTime;

        public void ChangeName(PositionName newName)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");
            Name = newName;

            LifeTime.UpdateUpdatedAt();
        }

        public void ChangeDescription(string? description)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Description = description;
            LifeTime.UpdateUpdatedAt();
        }

        public void Archive()
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            LifeTime.Archive();
        }


        public void Restore()
        {
            if (!LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование неархивированных запрещено");

            LifeTime.Restore();
        }
        public override bool Equals(object? obj) => obj is Position other && Id == other.Id;
        public override int GetHashCode() => Id.Value.GetHashCode();

        public static bool operator ==(Position? left, Position? right) =>
            left is not null && right is not null && left.Equals(right);

        public static bool operator !=(Position? left, Position? right) => !(left == right);
    }
}
