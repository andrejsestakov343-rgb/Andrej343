namespace Domain.LocationContext.ValueObjects;

public sealed record class LocationId(Guid Value)
{
    public LocationId() : this(Guid.NewGuid()) {}
}
