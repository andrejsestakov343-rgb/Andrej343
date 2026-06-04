using DirectoryService.Application.PositionContext.CreatePosition;
using DirectoryService.Application.PositionContext.UpdatePosition;
using DirectoryService.Application.PositionContext.DeletePosition;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.PositionContext;

[ApiController]
[Route("api/positions")]
public sealed class PositionController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreatePosition([FromBody] CreatePositionRequest request, [FromServices] CreatePositionHandler handler, CancellationToken ct)
    {
        var command = new CreatePositionCommand(request.Name);
        var result = await handler.Handle(command, ct);
        return Results.Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IResult> UpdatePosition(Guid id, [FromBody] UpdatePositionRequest request, [FromServices] UpdatePositionHandler handler, CancellationToken ct)
    {
        var command = new UpdatePositionCommand(id, request.Name);
        var result = await handler.Handle(command, ct);
        return Results.Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> DeletePosition(Guid id, [FromServices] DeletePositionHandler handler, CancellationToken ct)
    {
        var command = new DeletePositionCommand(id);
        var result = await handler.Handle(command, ct);
        return Results.Ok(result);
    }
}

public sealed record CreatePositionRequest(string Name);

public sealed record UpdatePositionRequest(string Name);
