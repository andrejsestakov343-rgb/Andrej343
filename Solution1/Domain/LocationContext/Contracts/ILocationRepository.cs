using Domain.LocationContext.ValueObjects;
using Domain.Locations;

namespace Domain.LocationContext.Contracts;

public interface ILocationRepository
{
    Task<bool> Exists(string locationName, CancellationToken ct = default);
    Task Add(Location location, CancellationToken ct = default);
    Task<Location?> GetById(Guid id, CancellationToken ct = default);
    Task<Location?> GetByName(LocationName name, CancellationToken ct = default);
    Task Update(Location location, CancellationToken ct = default);
    Task Update(Entities.Location location, CancellationToken ct);
    Task Add(Entities.Location location, CancellationToken ct);
}
