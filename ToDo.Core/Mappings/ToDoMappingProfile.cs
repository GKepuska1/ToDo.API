using AutoMapper;
using ToDo.Domain.Dtos.ToDo;
using ToDo.Domain.Entities.ToDo;

namespace ToDo.Core.Mappings;

public class ToDoMappingProfile : Profile
{
    public ToDoMappingProfile()
    {
        CreateMap<ToDoItem, ToDoDtoGet>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.DateCompleted, opt => opt.MapFrom(src => src.CompletedAt));

        CreateMap<ToDoDtoCreate, ToDoItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsCompleted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<ToDoDtoUpdate, ToDoItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
