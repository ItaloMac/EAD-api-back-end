using Application.DTOs.Admin.Aula;
using AutoMapper;

namespace Application.Mappers.Admin.AulaMappers;

public class AulaMapper : Profile
{
    public AulaMapper()
    {
        CreateMap<Domain.Models.Aula, AulaResponseDTO>()
            .ForMember(dest => dest.Modulo, opt => opt.MapFrom(src => new ModuloDTOAula
            {
                Id = src.Id_Modulo,
                Theme = src.Modulo.Theme
            }));

    }
}
