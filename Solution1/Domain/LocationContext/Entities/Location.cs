using Domain.LocationContext.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.LocationContext.Entities;

public sealed class Location
{
    public LocationId Id { get; }
    public LocationName Name { get; private set; }
    public LocationAddress Address { get; private set; }
    public LocationTimeZone TimeZone { get; }
    public EntityLifeTime LifeTime { get; private set; }

    public Location(LocationId id, LocationName name, LocationAddress address, LocationTimeZone timeZone, EntityLifeTime lifeTime)
    {
        Id = id;
        Name = name;
        Address = address;
        TimeZone = timeZone;
        LifeTime = lifeTime;
    }
    public void Update(LocationName? newName, LocationAddress? newAddress)
    {
        if (newName is null && newAddress is null)
        {
            string message = "Не предоставлены данные для обновления";
            throw new ArgumentException(message);
        }

        if (newName is not null)
        {

            if (LifeTime.IsArchived)
            {
                string message = "Невозможно обновить локацию, так как она архивирована";
                throw new InvalidOperationException(message);
            }

            Name = newName;
        }

        if (newAddress is not null)
        {
            Address = newAddress;
        }
        LifeTime = LifeTime.Update();
    }

    public static implicit operator Location?(Locations.Location? v)
    {
        throw new NotImplementedException();
    }

}
