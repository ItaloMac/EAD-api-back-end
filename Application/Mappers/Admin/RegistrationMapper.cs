using Application.DTOs.Admin.Registration;
using AutoMapper;
using Domain.Models;
using webAPI.Domain.Models;

namespace Application.Mappers.Admin;

public class RegistrationProfile : Profile
{
    public RegistrationProfile()
    {
        CreateMap<Registration, RegistrationResponseDTO>();

        CreateMap<Class, ClassDTO>();
        CreateMap<Curso, CourseDTO>();

        CreateMap<User, StudentDTO>();
    }
}
