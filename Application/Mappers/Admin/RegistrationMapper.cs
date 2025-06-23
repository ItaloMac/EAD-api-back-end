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
    }

    
}
