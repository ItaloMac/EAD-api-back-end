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

        CreateMap<CreateCourseDTO, Curso>()
        .ForMember(dest => dest.CoordenadorId, opt => opt.MapFrom(src => src.CoordenadorId));

        CreateMap<Curso, CreateCourseDTO>()
        .ForMember(dest => dest.CoordenadorId, opt => opt.MapFrom(src => new CoordenadorCourse { Id = src.Coordenador.Id }));

        CreateMap<UpdateCourseDTO, Curso>()
        .ForMember(dest => dest.CoordenadorId, opt => opt.MapFrom(src => src.CoordenadorId));

        CreateMap<Curso, UpdateCourseDTO>()
        .ForMember(dest => dest.CoordenadorId, opt => opt.MapFrom(src => new CoordenadorCourse { Id = src.Coordenador.Id }));

        CreateMap<CursoProfessor, CourseTeacherDTO>();
        CreateMap<CourseTeacherDTO, CursoProfessor>();

        CreateMap<Class, CourseClassListDTO>();
        CreateMap<CourseClassListDTO, Class>();
    }
}
