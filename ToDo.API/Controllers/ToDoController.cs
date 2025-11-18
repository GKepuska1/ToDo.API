using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Commands.ToDo;
using ToDo.Commands.ToDo.Queries;
using ToDo.Domain.Dtos.ToDo;

namespace ToDo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all TODO items
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllToDosQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get TODO item by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetToDoByIdQuery { Id = id });
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Get completed TODO items
    /// </summary>
    [HttpGet("completed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompleted()
    {
        var result = await _mediator.Send(new GetCompletedToDosQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get pending TODO items
    /// </summary>
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPending()
    {
        var result = await _mediator.Send(new GetPendingToDosQuery());
        return Ok(result);
    }

    /// <summary>
    /// Create new TODO item
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ToDoDtoCreate model)
    {
        var id = await _mediator.Send(new CreateToDoCommand() { ToDoDtoCreate = model });
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Update existing TODO item
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ToDoDtoUpdate model)
    {
        var result = await _mediator.Send(new UpdateToDoCommand() { Id = id, ToDoDtoUpdate = model} );
        if (!result)
            return NotFound();

        return Ok();
    }

    /// <summary>
    /// Mark TODO item as completed
    /// </summary>
    [HttpPatch("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Complete(int id)
    {
        var result = await _mediator.Send(new CompleteToDoCommand { Id = id });
        if (!result)
            return NotFound();

        return Ok();
    }

    /// <summary>
    /// Delete TODO item
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteToDoCommand { Id = id });
        if (!result)
            return NotFound();

        return NoContent();
    }
}
