using System;
using Application.DTOs.Admin.User;
using Domain.Models;

namespace Application.Mappers.Admin.UserMappers;

using AutoMapper;
using webAPI.Domain.Models;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.BirthDate, opt =>
                opt.MapFrom(src => src.BirthDate.HasValue ?
                    src.BirthDate.Value.ToString("dd-MM-yyyy") : null));

        CreateMap<UserCreateDTO, User>()
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Email));

        CreateMap<UserUpdateDTO, User>()
            .ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UserDeleteDTO, User>();


        CreateMap<Registration, UserRegistrationDTO>()
            .ForMember(dest => dest.RegistrationStatus, opt => opt.MapFrom(src => src.RegistrationStatus.ToString()));

        CreateMap<Class, ClassDTO>();
        CreateMap<Curso, CourseDTO>();
    }
}
