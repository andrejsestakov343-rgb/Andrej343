namespace Domain.PositionContext.ValueObjects;

public sealed record class PositionId (Guid Value)
{
    public PositionId() : this(Guid.NewGuid()) { }

    public static PositionId Create(Guid guid) => new(guid);
}
