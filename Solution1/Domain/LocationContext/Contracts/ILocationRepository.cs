using Domain.Locations;

namespace Domain.LocationContext.Contracts;

public interface ILocationRepository
{
    Task<bool> Exists(string locationName, CancellationToken ct = default);
    Task Add (Location location, CancellationToken ct = default);
}
