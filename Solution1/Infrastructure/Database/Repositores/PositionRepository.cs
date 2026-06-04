using Domain.PositionContext;
using Domain.PositionContext.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositores;

public sealed class PositionRepository(DirectoryDbContext context) : IPositionRepository
{
    private readonly DirectoryDbContext _context = context;

    public async Task<bool> Exists(string positionName, CancellationToken ct = default)
    {
        return await _context.Positions.AnyAsync(p => p.Name == positionName, ct);
    }

    public async Task Add(Position position, CancellationToken ct = default)
    {
        await _context.Positions.AddAsync(position, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Position?> GetById(Guid id, CancellationToken ct = default)
    {
        return await _context.Positions.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public Task<Position?> GetByName(string name, CancellationToken ct = default)
    {
        return _context.Positions.FirstOrDefaultAsync(p => p.Name == name, ct);
    }

    public async Task Update(Position position, CancellationToken ct = default)
    {
        _context.Positions.Update(position);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Delete(Position position, CancellationToken ct = default)
    {
        _context.Positions.Remove(position);
        await _context.SaveChangesAsync(ct);
    }
}



