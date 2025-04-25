using Api.Domain.Entities;
using AutoMapper;

namespace Api.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Todo, Todo>();
    }
}