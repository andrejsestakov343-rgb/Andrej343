using Domain.LocationContext;
using Domain.LocationContext.Contracts;

namespace DirectoryService.Application.LocationContext.UpdateLocation;

public sealed class UpdateLocationHandler
{
    private readonly ILocationRepository _repository;

    public UpdateLocationHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateLocationCommand command, CancellationToken ct = default)
    {
        Location? location = await _repository.GetById(command.Id, ct);

        if (location is null)
        {
            throw new InvalidOperationException("Локация не найдена");
        }

        if (!location.IsActive)
        {
            throw new InvalidOperationException("Нельзя обновить неактивную локацию");
        }

        if (command.NewName is not null)
        {
            Location? duplicate = await _repository.GetByName(command.NewName, ct);
            if (duplicate is not null && duplicate.Id != location.Id)
            {
                throw new InvalidOperationException($"Локация с именем {command.NewName} уже существует");
            }
        }

        if (command.NewName is not null)
            location.ChangeName(command.NewName);

        if (command.NewAddress is not null)
            location.ChangeAddress(command.NewAddress);

        await _repository.Update(location, ct);

        return location.Id;
    }
}