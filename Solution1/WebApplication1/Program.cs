using DirectoryService.Application.LocationContext.CreateLocation;
using DirectoryService.Application.LocationContext.UpdateLocation;
using DirectoryService.Application.PositionContext.CreatePosition;
using DirectoryService.Application.PositionContext.UpdatePosition;
using DirectoryService.Application.PositionContext.DeletePosition;
using Domain.LocationContext.Contracts;
using Domain.PositionContext.Contracts;
using Infrastructure.Database.Repositores;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<Infrastructure.DatabaseOptions>().BindConfiguration("Database");

builder.Services.AddScoped<CreatePositionHandler>();
builder.Services.AddScoped<CreateLocationHandler>();
builder.Services.AddScoped<UpdatePositionHandler>();
builder.Services.AddScoped<UpdateLocationHandler>();
builder.Services.AddScoped<DeletePositionHandler>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();
app.MapSwagger();

app.Run();
