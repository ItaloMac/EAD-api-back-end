using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class ProfessorMapper : Profile
{
    public ProfessorMapper()
    {
        CreateMap<Professor, ProfessorDTO>();
    }
}
