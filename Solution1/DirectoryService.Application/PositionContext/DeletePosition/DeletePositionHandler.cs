
using Domain.PositionContext.Contracts;

namespace DirectoryService.Application.PositionContext.DeletePosition;

public sealed class DeletePositionHandler(IPositionRepository repository)
{
    private readonly IPositionRepository _repository = repository;

    public async Task<Guid> Handle(DeletePositionCommand command, CancellationToken ct = default)
    {
        var position = await _repository.GetById(command.Id, ct);
        if (position is null)
        {
            string error = "Позиция не найдена.";
            throw new InvalidOperationException(error);
        }

        await _repository.Delete(position, ct);
        return position.Id;
    }
}
