using Domain.PositionContext.ValueObjects;

namespace Domain.PositionContext.Entities;

public sealed class Position
{
    public PositionId Id { get; }
    public PositionName Name { get; }
    public EntityLifeTime LifeTime { get; }

    public Position(PositionId id, PositionName name, EntityLifeTime lifeTime)
    {
        Id = id;
        Name = name;
        LifeTime = lifeTime;
    }
}
