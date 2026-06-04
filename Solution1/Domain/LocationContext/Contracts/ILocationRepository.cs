using Domain.LocationContext;

namespace Domain.LocationContext.Contracts;

public interface ILocationRepository
{
    Task<bool> Exists(string locationName, CancellationToken ct = default);
    Task Add(Location location, CancellationToken ct = default);
    Task<Location?> GetById(Guid id, CancellationToken ct = default);
    Task<Location?> GetByName(string name, CancellationToken ct = default);
    Task Update(Location location, CancellationToken ct = default);
    Task Delete(Location location, CancellationToken ct = default);
}
