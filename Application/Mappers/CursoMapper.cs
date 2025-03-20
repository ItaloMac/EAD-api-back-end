using AutoMapper;
using Application.DTOs;
using webAPI.Domain.Models;

namespace Application.Mappers;

public class CursoProfile : Profile
{
    public CursoProfile()
    {
        CreateMap<Curso, CursoDTO>();
    }
}
