using Domain.Locations.ValueObjects;

namespace Domain.Locations
{
    public class Location(
        LocationId id,
        LocationAddress address,
        LocationName name,
        IanaTimeZone timeZone,
        EntityLifeTime lifeTime
    )
    {
        public LocationId Id { get; } = id;
        public LocationAddress Address { get; private set; } = address;
        public LocationName Name { get; private set; } = name;
        public IanaTimeZone TimeZone { get; private set; } = timeZone;
        public EntityLifeTime LifeTime { get; } = lifeTime;

        public void СhangeТimeZone(string timeZone)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            TimeZone = IanaTimeZone.Create(timeZone);
            LifeTime.UpdateUpdatedAt();
        }

        public void ChangeName(string name)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Name = LocationName.Create(name);
            LifeTime.UpdateUpdatedAt();
        }


        public void ChangeAddress (string address)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Address = LocationAddress.Create(address);
            LifeTime.UpdateUpdatedAt();
        }
    }
}



