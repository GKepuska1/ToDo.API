using AutoMapper;
using MediatR;
using ToDo.Core.Services;
using ToDo.Domain.Dtos.ToDo;

namespace ToDo.Commands.ToDo.Queries;

public class GetToDoByIdQuery : IRequest<ToDoDtoGet?>
{
    public int Id { get; set; }
}

public class GetToDoByIdQueryHandler : IRequestHandler<GetToDoByIdQuery, ToDoDtoGet?>
{
    private readonly IToDoService _toDoService;
    private readonly IMapper _mapper;

    public GetToDoByIdQueryHandler(IToDoService service, IMapper mapper)
    {
        _toDoService = service;
        _mapper = mapper;
    }

    public async Task<ToDoDtoGet> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _toDoService.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<ToDoDtoGet>(item);
    }
}
