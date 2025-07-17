using Application.DTOs.Admin.Class;
using AutoMapper;
using Domain.Models;

namespace Application.Mappers.Admin.ClassMappers;

public class ClassProfile : Profile 
{
    public ClassProfile()
    {
        CreateMap<Class, ClassResponseDTO>()
            .ForMember(dest => dest.relacionedCourse, opt => opt.MapFrom(src => new RelacionedCourse
            {
                Id = src.Curso!.Id,
                Name = src.Curso.Name
            }))
            .ForMember(dest => dest.relacionedRegistrations, opt => opt.MapFrom(src =>
                src.Registrations.Select(r => new RelacionedRegistration
                {
                    Id = r.Id
                }).ToList()
            ));

        CreateMap<Class, CreateClassDTO>()
    .ForMember(dest => dest.RelatedCourse, opt => opt.MapFrom(src => new RelatedCourseDTO
    {
        Id = src.Curso!.Id
    }))
    .ForMember(dest => dest.RelatedRegistrations, opt => opt.MapFrom(src =>
        src.Registrations.Select(r => new RelatedRegistrationDTO
        {
            Id = r.Id
        }).ToList()
    ));
    }
}
