namespace DirectoryService.Application.LocationContext.CreateLocation;

public sealed record CreateLocationCommand (string Name, string Address, string IanaTimeZone);

