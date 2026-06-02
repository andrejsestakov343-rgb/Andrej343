using System.Diagnostics.Contracts;
using Domain.LocationContext.Contracts;
using Domain.LocationContext.Entities;
using Domain.LocationContext.ValueObjects;

namespace DirectoryService.Application.LocationContext.UpdateLocation;

public sealed class UpdateLocationHandler
{
    private readonly ILocationRepository _repository;

    public UpdateLocationHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateLocationCommand command,CancellationToken ct = default)
    {
        Location? location = await _repository.GetById(command.Id, ct);

        if (location is null)
        {
            string message = $"Локация не найдена";
            throw new InvalidOperationException("Локация не найдена");
        }

        LocationName? newName = command.NewName is not null ? LocationName.Create(command.NewName) : null;

        LocationAddress? newAddress = command.NewAddress is not null ? LocationAddress.Create(command.NewAddress) : null;

        if (newName is not null)
        {
            Location? duplicate = await _repository.GetByName(newName, ct);
            if (duplicate is not null && duplicate.Id != location.Id)
            {
                string message = $"Локация с именем {newName.Value} уже существует";
                throw new InvalidOperationException(message);
            }
        }

        location.Update(newName, newAddress);
        await _repository.Update(location, ct);

        return location.Id.Value;
    }
}