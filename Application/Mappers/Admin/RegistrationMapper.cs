using Application.DTOs.Admin.Registration;
using AutoMapper;
using Domain.Models;
using webAPI.Domain.Models;

namespace Application.Mappers.Admin;

public class RegistrationProfile : Profile
{
    public RegistrationProfile()
    {
        CreateMap<CreateRegistrationDTO, Registration>()
         .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.Class.Id))
         .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

        CreateMap<Registration, CreateRegistrationDTO>()
         .ForMember(dest => dest.Class, opt => opt.MapFrom(src => new ClassIdDTO { Id = src.ClassId }))
         .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserIdDTO { Id = src.UserId }));

        CreateMap<UpdateRegistrationDTO, Registration>()
        .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.Class.Id))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

        CreateMap<Registration, UpdateRegistrationDTO>()
         .ForMember(dest => dest.Class, opt => opt.MapFrom(src => new ClassIdDTO { Id = src.ClassId }))
         .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserIdDTO { Id = src.UserId }));
         
         CreateMap<Registration, RegistrationResponseDTO>()
    .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

    CreateMap<Class, ClassDTO>()
        .ForMember(dest => dest.Curso, opt => opt.MapFrom(src => src.Curso));

    CreateMap<Curso, CourseDTO>();

    CreateMap<User, StudentDTO>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
        .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

    }

    
}
