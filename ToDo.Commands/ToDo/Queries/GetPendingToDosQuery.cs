using AutoMapper;
using MediatR;
using ToDo.Core.Services;
using ToDo.Domain.Dtos.ToDo;

namespace ToDo.Commands.ToDo.Queries;

public class GetPendingToDosQuery : IRequest<List<ToDoDtoGet>>
{
}

public class GetPendingToDosQueryHandler : IRequestHandler<GetPendingToDosQuery, List<ToDoDtoGet>>
{
    private readonly IToDoService _toDoService;
    private readonly IMapper _mapper;

    public GetPendingToDosQueryHandler(IToDoService service, IMapper mapper)
    {
        _toDoService = service;
        _mapper = mapper;
    }

    public async Task<List<ToDoDtoGet>> Handle(GetPendingToDosQuery request, CancellationToken cancellationToken)
    {
        var items = await _toDoService.GetPendingAsync(cancellationToken);
        return _mapper.Map<List<ToDoDtoGet>>(items);
    }
}
