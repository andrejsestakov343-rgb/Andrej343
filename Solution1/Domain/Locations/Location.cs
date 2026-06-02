using System.Data.Common;
using Domain.Departments;
using Domain.Locations.ValueObjects;
using Domain.Shared.ValueObjects;
using static Domain.Locations.ValueObjects.IanaTimeZone;

namespace Domain.Locations
{
    public class Location
    {
        public LocationId Id { get; }
        public LocationAddress Address { get; private set; }
        public LocationName Name { get; private set; }
        public IanaTimeZone TimeZone { get; private set; }
        public EntityLifeTime LifeTime { get; private set; }

        public Location(LocationId id,
        LocationAddress address,
        LocationName name,
        IanaTimeZone timeZone,
        EntityLifeTime lifeTime)
        {
            Id = id;
            Address = address;
            Name = name;
            TimeZone = timeZone;
            LifeTime = lifeTime;
        }
        public Location ()
        {
            
        }

        public void СhangeТimeZone(string timeZone)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            TimeZone = IanaTimeZone.Create(timeZone);
            LifeTime = LifeTime.Update();
        }

        public void ChangeName(string name)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Name = LocationName.Create(name);
            LifeTime = LifeTime.Update();
        }


        public void ChangeAddress(string address)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Address = LocationAddress.Create(address);
            LifeTime = LifeTime.Update();
        }
        public ICollection<DepartmentLocation> DepartmentLocations { get; } = new List<DepartmentLocation>();
    }
}



