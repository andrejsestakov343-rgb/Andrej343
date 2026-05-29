using Domain.LocationContext.Contracts;
using Domain.Locations;
using Domain.Locations.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositores;

public sealed class LocationRepository : ILocationRepository
{
    private readonly DirectoryDbContext _context;

    public LocationRepository(DirectoryDbContext context)
    {
        _context = context;
    }

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
}

