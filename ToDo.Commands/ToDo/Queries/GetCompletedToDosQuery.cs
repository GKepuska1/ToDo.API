using AutoMapper;
using MediatR;
using ToDo.Core.Services;
using ToDo.Domain.Dtos.ToDo;

namespace ToDo.Commands.ToDo.Queries;

public class GetCompletedToDosQuery : IRequest<List<ToDoDtoGet>>
{
}

public class GetCompletedToDosQueryHandler : IRequestHandler<GetCompletedToDosQuery, List<ToDoDtoGet>>
{
    private readonly IToDoService _toDoService;
    private readonly IMapper _mapper;

    public GetCompletedToDosQueryHandler(IToDoService service, IMapper mapper)
    {
        _toDoService = service;
        _mapper = mapper;
    }

    public async Task<List<ToDoDtoGet>> Handle(GetCompletedToDosQuery request, CancellationToken cancellationToken)
    {
        var items = await _toDoService.GetCompletedAsync(cancellationToken);
        return _mapper.Map<List<ToDoDtoGet>>(items);
    }
}
