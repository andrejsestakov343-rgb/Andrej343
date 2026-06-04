
using Domain.PositionContext;
using Domain.PositionContext.Contracts;

namespace DirectoryService.Application.PositionContext.CreatePosition;

public sealed class CreatePositionHandler(IPositionRepository repository)
{
    private readonly IPositionRepository _repository = repository;


    public async Task<Guid> Handle(CreatePositionCommand command, CancellationToken ct = default)
    {
        ValidateName(command.Name);
        await ValidateUniqueness(command.Name, ct);
        var position = CreatePosition(command.Name);
        await _repository.Add(position, ct);
        return position.Id;
    }
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название позиции не может быть пустым");

        const int maxLength = 255;
        if (name.Length > maxLength)
            throw new ArgumentException($"Название позиции не может быть длиннее {maxLength}.");
    }
    private async Task ValidateUniqueness(string name, CancellationToken ct)
    {
        if (await _repository.Exists(name, ct))
            throw new InvalidOperationException($"Позиция '{name}' уже существует.");
    }
    private static Position CreatePosition(string name)
    {
        var id = Guid.NewGuid();
        var now = DateTime.UtcNow;

        return new Position(id, name, null, now, now);
    }

}

