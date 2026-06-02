using Domain.Positions;
using Domain.Positions.ValueObjects;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Database.Repositores;

public sealed class PositionRepository(DirectoryDbContext context) : IPositionRepository
{
    private readonly DirectoryDbContext _context = context;

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
    public async Task<Position?> GetById(Guid id, CancellationToken ct = default)
{
    var positionId = new PositionId(id);
    return await _context.Positions.FirstOrDefaultAsync(p => p.Id == positionId, ct);
}

public Task<Position?> GetByName(PositionName name, CancellationToken ct = default)
{
    return _context.Positions.FirstOrDefaultAsync(p => p.Name == name, ct);
}

public async Task Update(Position position, CancellationToken ct = default)
{
    _context.Positions.Update(position);
    await _context.SaveChangesAsync(ct);
}

    public Task<bool> ExistsByNameAsync(PositionName name)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Position position)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

}



