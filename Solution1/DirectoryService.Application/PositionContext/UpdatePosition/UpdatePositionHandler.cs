
using Domain.PositionContext;
using Domain.PositionContext.Contracts;

namespace DirectoryService.Application.PositionContext.UpdatePosition;

public sealed class UpdatePositionHandler(IPositionRepository repository)
{
     private readonly IPositionRepository _repository = repository;


    public async Task<Guid> Handle(UpdatePositionCommand command,
    CancellationToken ct = default)
    {
        Position? position = await _repository.GetById(command.Id, ct);
        if (position is null)
        {
            string message = $"Должность не найдена";
            throw new InvalidOperationException(message);
        }
        
        if (!position.IsActive)
        {
            string message = "Нельзя обновить неактивную должность";
            throw new InvalidOperationException(message);
        }
        
        string name = command.Name;
        Position? duplicate = await _repository.GetByName(name, ct);
        if (duplicate is not null && duplicate.Id != position.Id)
        {
            string message = $"Должность с таким именем {name} уже существует";
            throw new InvalidOperationException(message);
        }
        
        position.ChangeName(name);
        await _repository.Update(position, ct);
        return position.Id;
    }
}
