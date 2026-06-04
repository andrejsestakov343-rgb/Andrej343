
using Domain.LocationContext;
using Domain.LocationContext.Contracts;

namespace DirectoryService.Application.LocationContext.CreateLocation;

public sealed class CreateLocationHandler(ILocationRepository repository)
{
    private readonly ILocationRepository _repository = repository;

    public async Task<Guid> Handle(CreateLocationCommand command, CancellationToken ct = default)
    {
        ValidateFields(command);
        await ValidateUniqueness(command.Name, ct);
        var location = CreateLocation(command);
        await _repository.Add(location, ct);
        return location.Id;
    }

    private static void ValidateFields(CreateLocationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new ArgumentException("Название локации не может быть пустым.");

        if (string.IsNullOrWhiteSpace(command.Address))
            throw new ArgumentException("Адрес не может быть пустым.");

        if (string.IsNullOrWhiteSpace(command.IanaTimeZone))
            throw new ArgumentException("Часовой пояс не может быть пустым.");

        const int maxLength = 255;
        if (command.Name.Length > maxLength || command.Address.Length > maxLength || command.IanaTimeZone.Length > maxLength)
            throw new ArgumentException("Поля не могут быть длиннее 255 символов.");
    }

    private async Task ValidateUniqueness(string name, CancellationToken ct)
    {
        if (await _repository.Exists(name, ct))
            throw new InvalidOperationException($"Локация '{name}' уже существует.");
    }

    private static Location CreateLocation(CreateLocationCommand command)
    {
        var id = Guid.NewGuid();
        var now = DateTime.UtcNow;

        return new Location(id, command.Name, command.Address, command.IanaTimeZone, now, now);
    }
}








