using System;
using AutoMapper;
using Domain;
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
