using Domain.PositionContext.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.PositionContext.Entities;

public sealed class Position
{
    public PositionId Id { get; }
    public PositionName Name { get; private set; }
    public EntityLifeTime LifeTime { get; private set; }

    public Position(PositionId id, PositionName name, EntityLifeTime lifeTime)
    {
        Id = id;
        Name = name;
        LifeTime = lifeTime;
    }
    public void Rename (PositionName name)
    {
        if (LifeTime.IsArchived)
        {
            string message = "Невозможно переименовать должность, так как она уже архивирована";
            throw new InvalidOperationException(message);
        }
        Name = name;
        LifeTime = LifeTime.Update();
    }

    public static implicit operator Position?(Positions.Position? v)
    {
        throw new NotImplementedException();
    }

}
