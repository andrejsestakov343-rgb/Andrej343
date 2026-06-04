using Domain.PositionContext;

namespace Domain.PositionContext.Contracts;

public interface IPositionRepository
{
    Task<bool> Exists(string positionName, CancellationToken ct = default);
    Task Add(Position position, CancellationToken ct = default);
    Task<Position?> GetById(Guid id, CancellationToken ct = default);
    Task<Position?> GetByName(string name, CancellationToken ct = default);
    Task Update(Position position, CancellationToken ct = default);
    Task Delete(Position position, CancellationToken ct = default);
}