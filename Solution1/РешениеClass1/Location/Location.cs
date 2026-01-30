using Domain.LocationsContext.ValueObjects;
using РешениеClass1.Location.ValueObjects;

namespace РешениеClass1.Location
{
    public class Location
    {
        public Location(
            LocationId id,
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

        public LocationId Id { get; }
        public LocationAddress Address { get; }
        public LocationName Name { get; }
        public EntityLifeTime LifeTime { get; }
        public IanaTimeZone TimeZone { get; }
    }
    

        
     
       
    
}















