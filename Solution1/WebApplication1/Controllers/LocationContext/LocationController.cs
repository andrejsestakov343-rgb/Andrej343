using DirectoryService.Application.LocationContext.CreateLocation;
using DirectoryService.Application.LocationContext.UpdateLocation;
using Domain.LocationContext.Contracts;
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
        return Results.Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetLocation(Guid id, [FromServices] ILocationRepository repository, CancellationToken ct)
    {
        var location = await repository.GetById(id, ct);
        if (location is null)
            return Results.NotFound(new { message = $"Локация с ID {id} не найдена" });

        return Results.Ok(new { location.Id, location.Name, location.Address, location.TimeZone, location.IsActive, location.CreatedAt, location.UpdatedAt });
    }

    [HttpPut("{id:guid}")]
    public async Task<IResult> UpdateLocation(Guid id, [FromBody] UpdateLocationRequest request, [FromServices] UpdateLocationHandler handler, CancellationToken ct)
    {
        var command = new UpdateLocationCommand(id, request.NewName, request.NewAddress);
        var result = await handler.Handle(command, ct);
        return Results.Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> DeleteLocation(Guid id, [FromServices] ILocationRepository repository, CancellationToken ct)
    {
        var location = await repository.GetById(id, ct);
        if (location is null)
            return Results.NotFound(new { message = $"Локация с ID {id} не найдена" });

        if (location.IsActive)
            return Results.BadRequest(new { message = "Удаление активных локаций запрещено" });

        await repository.Delete(location, ct);
        return Results.Ok(new { message = "Локация удалена", id });
    }
}

public sealed record CreateLocationRequest(string Name, string Address, string IanaTimeZone);

public sealed record UpdateLocationRequest(string? NewName, string? NewAddress);





