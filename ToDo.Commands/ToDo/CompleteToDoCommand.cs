using MediatR;
using ToDo.Core.Services;

namespace ToDo.Commands.ToDo;

public class CompleteToDoCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class CompleteToDoCommandHandler : IRequestHandler<CompleteToDoCommand, bool>
{
    private readonly IToDoService _toDoService;

    public CompleteToDoCommandHandler(IToDoService service)
    {
        _toDoService = service;
    }

    public async Task<bool> Handle(CompleteToDoCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoService.GetByIdAsync(request.Id, cancellationToken);
        if (toDoItem == null)
        {
            throw new KeyNotFoundException($"ToDo item with Id {request.Id} not found.");
        }

        toDoItem.IsCompleted = true;
        toDoItem.CompletedAt = DateTime.UtcNow;
        toDoItem.UpdatedAt = DateTime.UtcNow;

        return await _toDoService.CompleteAsync(toDoItem, cancellationToken);
    }
}
