using Domain.PositionContext.Contracts;
using Domain.Positions;
namespace Domain.PositionContext.Contracts;

public interface IPositionRepository
{
    Task<bool> Exists(string positionName, CancellationToken ct = default);
    Task Add(Position position, CancellationToken ct = default);
}
