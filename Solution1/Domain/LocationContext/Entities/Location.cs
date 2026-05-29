using Domain.LocationContext.ValueObjects;

namespace Domain.LocationContext.Entities;

public sealed class Location
{
    public LocationId Id { get; }
    public LocationName Name { get; }
    public LocationAddress Address { get; }
    public LocationTimeZone TimeZone { get; }
    public EntityLifeTime LifeTime { get; }

    public Location(LocationId id, LocationName name, LocationAddress address, LocationTimeZone timeZone, EntityLifeTime lifeTime)
    {
        Id = id;
        Name = name;
        Address = address;
        TimeZone = timeZone;
        LifeTime = lifeTime;
    }
}