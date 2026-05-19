using System.Data.Common;
using Domain.Departments;
using Domain.Locations.ValueObjects;

namespace Domain.Locations
{
    public class Location
    {
        public LocationId Id { get; }
        public LocationAddress Address { get; private set; }
        public LocationName Name { get; private set; } 
        public IanaTimeZone TimeZone { get; private set; }
        public EntityLifeTime LifeTime { get; }

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
            LifeTime.UpdateUpdatedAt();
        }

        public void ChangeName(string name)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Name = LocationName.Create(name);
            LifeTime.UpdateUpdatedAt();
        }


        public void ChangeAddress(string address)
        {
            if (LifeTime.IsArchived)
                throw new InvalidOperationException("Редактирование архивированных запрещено");

            Address = LocationAddress.Create(address);
            LifeTime.UpdateUpdatedAt();
        }
        public ICollection<DepartmentLocation> DepartmentLocations { get; } = new List<DepartmentLocation>();
    }
}



