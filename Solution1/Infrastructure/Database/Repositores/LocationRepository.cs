using Domain.LocationContext.Contracts;
using Domain.Locations;
using Domain.Locations.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositores;

public sealed class LocationRepository(DirectoryDbContext context) : ILocationRepository
{
    private readonly DirectoryDbContext _context = context;

    public async Task<bool> Exists(string locationName, CancellationToken ct = default)
    {
        var name = LocationName.Create(locationName);
        var nameValue = name.Value;
        return await _context.Locations.AnyAsync(l => l.Name.Value == name.Value, ct);
    }

    public async Task Add(Location location, CancellationToken ct = default)
    {
        await _context.Locations.AddAsync(location, ct);
        await _context.SaveChangesAsync(ct);
    }
    public async Task<Location?> GetById(Guid id, CancellationToken ct = default)
    {
        var locationId = LocationId.Create(id);
        return await _context.Locations.FirstOrDefaultAsync(l => l.Id == locationId, ct);
    }

    public Task<Location?> GetByName(LocationName name, CancellationToken ct = default)
    {
        return _context.Locations.FirstOrDefaultAsync(l => l.Name == name, ct);
    }

    public async Task Update(Location location, CancellationToken ct = default)
    {
        _context.Locations.Update(location);
        await _context.SaveChangesAsync(ct);
    }
    public Task Update(Domain.LocationContext.Entities.Location location, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Location?> GetByName(Domain.LocationContext.ValueObjects.LocationName name, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task Add(Domain.LocationContext.Entities.Location location, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

}





