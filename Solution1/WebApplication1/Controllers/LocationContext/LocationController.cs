using DirectoryService.Application.LocationContext.CreateLocation;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.LocationContext;

[ApiController]
[Route("api/locations")]
public sealed class LocationController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateLocation([FromBody] CreateLocationRequest request, [FromServices] CreateLocationHandler handler, CancellationToken ct)
    {
        var command = new CreateLocationCommand(request.Name, request.Address, request.IanaTimeZone);
        var result = await handler.Handle(command, ct);
        return Results.Ok(await handler.Handle(command, ct));
    }
    public sealed record CreateLocationRequest(string Name, string Address, string IanaTimeZone);
}





