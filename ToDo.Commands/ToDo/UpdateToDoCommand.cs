using MediatR;
using ToDo.Core.Services;
using ToDo.Domain.Dtos.ToDo;
using ToDo.Domain.Entities.ToDo;

namespace ToDo.Commands.ToDo;

public class UpdateToDoCommand : IRequest<bool>
{
    public int Id { get; set; }
    public ToDoDtoUpdate ToDoDtoUpdate { get; set; }
}

public class UpdateToDoCommandHandler : IRequestHandler<UpdateToDoCommand, bool>
{
    private readonly IToDoService _toDoService;

    public UpdateToDoCommandHandler(IToDoService service)
    {
        _toDoService = service;
    }

    public async Task<bool> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        if(request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var todoItem = await _toDoService.GetByIdAsync(request.Id, cancellationToken);
        if(todoItem == null)
        {
            throw new KeyNotFoundException($"ToDo item with Id {request.Id} not found.");
        }

        if(todoItem.Title != null && todoItem.Title != request.ToDoDtoUpdate.Title)
        {
            todoItem.Title = request.ToDoDtoUpdate.Title;
        }

        if(todoItem.Description != null && todoItem.Description != request.ToDoDtoUpdate.Description)
        {
            todoItem.Description = request.ToDoDtoUpdate.Description;
        }

        if(todoItem.IsCompleted != request.ToDoDtoUpdate.IsCompleted)
        {
            todoItem.IsCompleted = request.ToDoDtoUpdate.IsCompleted;
        }

        if (!todoItem.IsCompleted && request.ToDoDtoUpdate.IsCompleted)
        {
            todoItem.CompletedAt = DateTime.UtcNow;
        }

        return await _toDoService.UpdateAsync(todoItem,
            cancellationToken);
    }
}
