using Microsoft.AspNetCore.Mvc;
using Domain.Positions.ValueObjects;
using Domain.Positions;
using Microsoft.VisualBasic;

namespace WebApplication1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PositionController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var position = ImitatyaBazaDannya.GetById(new PositionId(id));
        if (position == null)
            return NotFound(new { message = $"Должность с ID {id} не найдена" });

        return Ok(position);
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreatePositionForm form)
    {
        var positionName = PositionName.Create(form.PositionName);
        var description = PositionDescription.Create(form.PositionDescription);
        var positionId = PositionId.New();
        var lifeTime = new EntityLifeTime();
        var position = new Position(positionId, positionName, description, lifeTime);
        ImitatyaBazaDannya.Add(position);
        return Created($"/api/positions/{position.Id}", position);

    }
    [HttpPatch("{id}")]
    public IActionResult Update( Guid id, [FromBody] UpdatePositionForm form)
    {
        var positionName = PositionName.Create(form.PositionName);
        var description = PositionDescription.Create(form.PositionDescription);
        var positionId = new PositionId(id);
        var lifeTime = new EntityLifeTime();
        var position = new Position(positionId, positionName, description, lifeTime);
        ImitatyaBazaDannya.UpdatePosition(position);
        return Ok(position);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var position = ImitatyaBazaDannya.GetById(new PositionId(id));
        if (position == null)
            return NotFound(new { message = $"Должность с ID {id} не найдена" });

        if (!ImitatyaBazaDannya.EntityArchive(position))
            return BadRequest(new { message = "Удаление архивированных запрещено" });

        ImitatyaBazaDannya.RemovePosition(new PositionId (id));
        return NoContent();
    }
}
