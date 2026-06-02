using Microsoft.AspNetCore.Mvc;
using Domain.Locations;
using Domain.Locations.ValueObjects;

namespace WebApplication1.Controllers;
[ApiController]
[Route("api/locations")]
public class LocationController : ControllerBase
{
   [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var location = ImitatyaBazaDannya.GetById(new LocationId(id));
        if (location == null)
            return NotFound(new { message = $"Местоположение с ID {id} не найдено" });

        return Ok(location);
    }

   [HttpPost]
    public IActionResult Create(Location location)
    {
        if (location == null)
            return BadRequest(new { message = "Не указаны данные места" });

        try
        {
            ImitatyaBazaDannya.Add(location);
            return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
        }
        catch (ArgumentException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

  [HttpPatch("{id}")]
    public IActionResult Update(Guid id, Location updateLocation)
    {
       var locationId = LocationId.Create(id);

        if (locationId != updateLocation.Id)
            return BadRequest(new { message = "ID в теле и URL должны совпадать" });

        var existing = ImitatyaBazaDannya.GetById(new LocationId(id));
        if (existing == null)
            return NotFound(new { message = $"Местоположение с ID {id} не найдено" });

        if (!ImitatyaBazaDannya.EntityArchive(existing))
            return BadRequest(new { message = "Редактирование архивированных запрещено" });

        try
        {
            ImitatyaBazaDannya.UpdateLocation(updateLocation);
            return Ok(updateLocation);
        }
        catch (ArgumentException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

  [HttpDelete("{id}")]
public IActionResult Delete(Guid id)
{
    var locationId = LocationId.Create(id);
    var location = ImitatyaBazaDannya.GetById(locationId);
    if (location == null)
        return NotFound(new { message = $"Местоположение с ID {id} не найдено" });

    if (!ImitatyaBazaDannya.EntityArchive(location))
        return BadRequest(new { message = "Удаление архивированных запрещено" });

        ImitatyaBazaDannya.RemoveLocation(locationId);
        return NoContent();
    }
}
