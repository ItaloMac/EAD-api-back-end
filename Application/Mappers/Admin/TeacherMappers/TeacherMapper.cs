using System;
using Application.DTOs.Admin.Teacher;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers.Admin.TeacherMappers;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<Professor, TeacherResponseDTO>();
        CreateMap<TeacherResponseDTO, Professor>();

        CreateMap<Professor, CreateTeacherDTO>();
        CreateMap<CreateTeacherDTO, Professor>();
    }
}
