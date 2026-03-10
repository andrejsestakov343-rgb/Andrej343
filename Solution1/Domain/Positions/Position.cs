using Domain.Positions.ValueObjects;

namespace Domain.Positions
{
    public class Position
    {
        public PositionId Id { get; private set; }
        public PositionName Name { get; private set; }
        public string? Description { get; private set; } // или PositionDescription, если хотите value object
        
        public EntityLifeTime LifeTime { get; private set; }

        

        public Position(PositionId id, PositionName name, string? description, EntityLifeTime lifeTime)
        {
            Id = id;
            Name = name;
            Description = description;
            LifeTime = lifeTime;
            
        }

        public void ChangeName(PositionName  newName)
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
        // Equality
        public override bool Equals(object? obj) => obj is Position other && Id == other.Id; // ← теперь работает, т.к. PositionId — record с ==

        public override int GetHashCode() => Id.Value.GetHashCode(); // ← ключевая исправленная строка!

        public static bool operator ==(Position? left, Position? right) =>
            left is not null && right is not null && left.Equals(right);

        public static bool operator !=(Position? left, Position? right) => !(left == right);
    }
}
