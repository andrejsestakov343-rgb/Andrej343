using System;
using System.Collections.Generic;
using System.Text;
using РешениеClass1.Position.ValueObjects;

namespace РешениеClass1.Position
{
    public class Position
    {
        public PositionId Id { get; private set; }
        public PositionName Name { get; private set; }
        public string? Description { get; private set; } // или PositionDescription, если хотите value object
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Position(PositionId id, PositionName name, string? description = null, bool isActive = true)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }


        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }


        // Equality
        public override bool Equals(object? obj) =>
            obj is Position other && Id == other.Id; // ← теперь работает, т.к. PositionId — record с ==

        public override int GetHashCode() => Id.Value.GetHashCode(); // ← ключевая исправленная строка!

        public static bool operator ==(Position? left, Position? right) =>
            left is not null && right is not null && left.Equals(right);

        public static bool operator !=(Position? left, Position? right) =>
            !(left == right);
    }
}

