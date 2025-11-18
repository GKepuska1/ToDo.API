using AutoMapper;
using MediatR;
using ToDo.Core.Services;
using ToDo.Domain.Dtos.ToDo;

namespace ToDo.Commands.ToDo.Queries;

public class GetAllToDosQuery : IRequest<List<ToDoDtoGet>>
{
}

public class GetAllToDosQueryHandler : IRequestHandler<GetAllToDosQuery, List<ToDoDtoGet>>
{
    private readonly IToDoService _toDoService;
    private readonly IMapper _mapper;

    public GetAllToDosQueryHandler(IToDoService service, IMapper mapper)
    {
        _toDoService = service;
        _mapper = mapper;
    }

    public async Task<List<ToDoDtoGet>> Handle(GetAllToDosQuery request, CancellationToken cancellationToken)
    {
        var items = await _toDoService.GetAllAsync(cancellationToken);
        return _mapper.Map<List<ToDoDtoGet>>(items);
    }
}
