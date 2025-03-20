using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class ModuloMapper : Profile
{
    public ModuloMapper()
    {
        CreateMap<Modulo, ModuloDTO>();
    }
}
