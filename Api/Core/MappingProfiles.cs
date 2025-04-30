using Api.Domain.Entities;
using Api.DTOs;
using AutoMapper;

namespace Api.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Todo, Todo>();
        CreateMap<CreateTodoDto, Todo>();
        CreateMap<EditTodoDto, Todo>();
    }
}