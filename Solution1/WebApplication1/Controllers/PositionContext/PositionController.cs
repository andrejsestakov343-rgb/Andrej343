using DirectoryService.Application.PositionContext.CreatePosition;
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
        return Results.Ok (result);
    }
}
public sealed record CreatePositionRequest(string Name);
