using Application.DTOs.Admin.Aula;
using Application.DTOs.Admin.Module;
using AutoMapper;

namespace Application.Mappers.Admin.AulaMappers;

public class AulaMapper : Profile
{
    public AulaMapper()
    {
        CreateMap<Domain.Models.Aula, AulaResponseDTO>()
            .ForMember(dest => dest.Modulo, opt => opt.MapFrom(src => new ModuloDTOAula
            {
                Id = src.ModuloId,
                Theme = src.Modulo.Theme
            }));

        CreateMap<CreateAulaDTO, Domain.Models.Aula>()
            .ForMember(dest => dest.ModuloId, opt => opt.MapFrom(src => src.ModuloId));

        CreateMap<Domain.Models.Aula, CreateAulaDTO>()
            .ForMember(dest => dest.ModuloId, opt => opt.MapFrom(src => src.ModuloId));
   }
}
