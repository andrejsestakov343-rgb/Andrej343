using System.Windows.Input;
namespace DirectoryService.Application.LocationContext.UpdateLocation;

public sealed record UpdateLocationCommand(Guid Id, string? NewName, string? NewAddress);
