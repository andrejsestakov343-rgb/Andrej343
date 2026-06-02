using Domain.PositionContext.Contracts;
using Domain.PositionContext.Entities;
using Domain.PositionContext.ValueObjects;

namespace Domain.PositionContext.Contracts;

public interface IPositionRepository
{
    Task<bool> Exists(string positionName, CancellationToken ct = default);
    Task Add(Position position, CancellationToken ct = default);
    Task<Position?> GetById(Guid id, CancellationToken ct = default);
    Task<Position?> GetByName(PositionName name, CancellationToken ct = default);
    Task Update(Position position, CancellationToken ct = default);
}