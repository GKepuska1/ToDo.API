using MediatR;
using ToDo.Core.Services;

namespace ToDo.Commands.ToDo;

public class DeleteToDoCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, bool>
{
    private readonly IToDoService _toDoService;

    public DeleteToDoCommandHandler(IToDoService service)
    {
        _toDoService = service;
    }

    public async Task<bool> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoService.GetByIdAsync(request.Id, cancellationToken);
        if (toDoItem == null)
        {
            throw new KeyNotFoundException($"ToDo item with Id {request.Id} not found.");
        }

        return await _toDoService.DeleteAsync(toDoItem, cancellationToken);
    }
}
