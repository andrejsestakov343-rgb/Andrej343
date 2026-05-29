namespace Domain.Positions.ValueObjects;

public interface IPositionRepository
{
    Task<bool> ExistsByNameAsync(PositionName name);
    Task AddAsync(Position position);
    Task SaveChangesAsync();
}
