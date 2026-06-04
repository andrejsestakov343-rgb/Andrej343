using Domain.LocationContext;
using Domain.LocationContext.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Infrastructure.Database.Repositores;

public sealed class LocationRepository(DirectoryDbContext context) : ILocationRepository
{
    private readonly DirectoryDbContext _context = context;

    public async Task<bool> Exists(string locationName, CancellationToken ct = default)
    {
        return await _context.Locations.AnyAsync(l => l.Name == locationName, ct);
    }

    public async Task Add(Location location, CancellationToken ct = default)
    {
        await _context.Locations.AddAsync(location, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Location?> GetById(Guid id, CancellationToken ct = default)
    {
        return await _context.Locations.FirstOrDefaultAsync(l => l.Id == id, ct);
    }

    public Task<Location?> GetByName(string name, CancellationToken ct = default)
    {
        return _context.Locations.FirstOrDefaultAsync(l => l.Name == name, ct);
    }

    public async Task Update(Location location, CancellationToken ct = default)
    {
        _context.Locations.Update(location);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Delete(Location location, CancellationToken ct = default)
    {
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync(ct);
    }
}





