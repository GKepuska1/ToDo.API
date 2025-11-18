using AutoMapper;
using MediatR;
using ToDo.Core.Services;
using ToDo.Domain.Dtos.ToDo;
using ToDo.Domain.Entities.ToDo;

namespace ToDo.Commands.ToDo;

public class CreateToDoCommand : IRequest<int>
{
    public ToDoDtoCreate ToDoDtoCreate { get; set; }
}

public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, int>
{
    private readonly IToDoService _toDoService;
    private readonly IMapper _mapper;

    public CreateToDoCommandHandler(IToDoService toDoService,
        IMapper mapper)
    {
        _toDoService = toDoService;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        if(request == null || request.ToDoDtoCreate == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if(string.IsNullOrEmpty(request.ToDoDtoCreate.Title))
        {
            throw new ArgumentException("Title cannot be null or empty", nameof(request.ToDoDtoCreate.Title));
        }

        var toDoItem = _mapper.Map<ToDoItem>(request.ToDoDtoCreate);
        var  toDoItemId = await _toDoService.CreateAsync(toDoItem, cancellationToken);

        return toDoItemId;
    }
}
