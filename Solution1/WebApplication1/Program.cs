using DirectoryService.Application.LocationContext.CreateLocation;
using DirectoryService.Application.PositionContext.CreatePosition;
using Domain.LocationContext.Contracts;
using Domain.Locations;
using Domain.Locations.ValueObjects;
using Domain.Positions;
using Domain.Positions.ValueObjects;
using Infrastructure;
using Infrastructure.Database.Repositores;
using WebApplication1;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<Infrastructure.DatabaseOptions>().BindConfiguration("Database");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CreatePositionHandler>();
builder.Services.AddScoped<CreateLocationHandler>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/locations/{id}", (Guid id) =>
{
    LocationId locationId = id;
    var location = ImitatyaBazaDannya.GetById(locationId);
    if (location == null)
        return Results.NotFound(new { message = $"Локация с ID {id} не найдена" });

    return Results.Ok(location);
});

app.MapGet("/api/positions/{id}", (Guid id) =>
{
    var position = ImitatyaBazaDannya.GetById(new PositionId(id));
    if (position == null)
        return Results.NotFound(new { message = $"Должность с ID {id} не найдена" });

    return Results.Ok(position);
});

app.MapPost("/api/locations", (Location newLocation) =>
{
    try
    {
        ImitatyaBazaDannya.Add(newLocation);

        return Results.Created($"/api/locations/{newLocation.Id}", newLocation);
    }
    catch (ArgumentException ex)
    {

        return Results.Conflict(new { message = ex.Message });
    }
    catch (Exception ex)
    {

        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/api/positions", (Position newPosition) =>
{
    try
    {
        ImitatyaBazaDannya.Add(newPosition);

        return Results.Created($"/api/positions/{newPosition.Id}", newPosition);
    }
    catch (ArgumentException ex)
    {
        return Results.Conflict(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPatch("/api/locations/{id}", (Guid id, Location updateLocation) =>
{
    LocationId locationId = id;
    try
    {
        var existingLocation = ImitatyaBazaDannya.GetById(locationId);
        if (existingLocation == null)
            return Results.NotFound(new { message = $"Локация с ID {id} не найдена" });

        if (existingLocation.Name != updateLocation.Name)
        {
            var allLocations = ImitatyaBazaDannya.GetAllLocations();
            if (allLocations.Any(l => l.Name == updateLocation.Name && l.Id != locationId))
                return Results.Conflict(new { message = "Локация с таким названием уже существует" });
        }
        ImitatyaBazaDannya.UpdateLocation(existingLocation);
        return Results.Ok(existingLocation);
    }
    catch (ArgumentException ex)
    {
        return Results.Conflict(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPatch("/api/positions/{id}", (Guid id, Position updatePosition) =>
{
    PositionId positionId = new PositionId (id);
    try
    {
        var existingPosition = ImitatyaBazaDannya.GetById(positionId);
        if (existingPosition == null)
            return Results.NotFound(new { message = $"Должность с ID {id} не найдена" });

        if (existingPosition.Name != updatePosition.Name)
        {
            var allPositions = ImitatyaBazaDannya.GetAllPositions();
            if (allPositions.Any(p => p.Name == updatePosition.Name && p.Id != positionId))
                return Results.Conflict(new { message = "Должность с таким названием уже существует" });
        }

        ImitatyaBazaDannya.UpdatePosition(existingPosition);
        return Results.Ok(existingPosition);
    }
    catch (ArgumentException ex)
    {
        return Results.Conflict(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});
app.MapDelete("/api/locations/{id}", (Guid id) =>
{
    LocationId locationId = id;
    var location = ImitatyaBazaDannya.GetById(locationId);
    if (location == null)
        return Results.NotFound(new { message = $"Локация с ID {id} не найдена" });

    if (!ImitatyaBazaDannya.EntityArchive(location))
        return Results.BadRequest(new { message = "Удаление архивированных запрещено" });

    ImitatyaBazaDannya.RemoveLocation(locationId);
    return Results.NoContent();
});

app.MapDelete("/api/positions/{id}", (Guid id) =>
{
    PositionId positionId = new PositionId (id);

    var position = ImitatyaBazaDannya.GetById(positionId);

    if (position == null)
        return Results.NotFound(new { message = $"Должность с ID {id} не найдена" });

    if (!ImitatyaBazaDannya.EntityArchive(position))
        return Results.BadRequest(new { message = "Удаление архивированных запрещено" });

    ImitatyaBazaDannya.RemovePosition(positionId);
    return Results.NoContent();
});

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();
app.MapSwagger();

app.Run();
