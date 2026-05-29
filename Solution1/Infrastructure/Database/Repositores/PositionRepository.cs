using Domain.Positions;
using Domain.Positions.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Database.Repositores;

public sealed class PositionRepository : IPositionRepository
{
    private readonly DirectoryDbContext _context;

    public PositionRepository(DirectoryDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Exists(string positionName, CancellationToken ct = default)
    {
        var name = PositionName.Create(positionName);
        return await _context.Positions.AnyAsync(p => p.Name == name, ct);
    }

    public async Task Add(Position position, CancellationToken ct = default)
    {
        await _context.Positions.AddAsync(position, ct);
        await _context.SaveChangesAsync(ct);
    }
}



