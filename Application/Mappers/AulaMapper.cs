using System;
using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers;

public class AulaMapper : Profile
{
    public AulaMapper()
    {
        CreateMap<Aula, AulaDTO>();
    }
}
