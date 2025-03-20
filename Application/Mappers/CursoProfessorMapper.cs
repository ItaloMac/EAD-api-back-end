using System;
using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class CursoProfessorMapper : Profile
{
    public CursoProfessorMapper()
    {
        CreateMap<CursoProfessor, CursoProfessorDTO>();
    }
}
