using System;
using Application.DTOs.Admin.Course;
using AutoMapper;
using Domain.Models;
using webAPI.Domain.Models;

namespace Application.Mappers.Admin.CourseMappers;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
    CreateMap<Curso, CoursesReponseDTO>()
        .ForMember(dest => dest.Coordenador, opt => opt.MapFrom(src => src.Coordenador));

    CreateMap<Professor, CoordenadorCourse>();
    }
}
