using System;
using Application.DTOs.Admin.Module;
using AutoMapper;

namespace Application.Mappers.Admin.ModuleMappers;

public class ModuleProfile : Profile
{
    public ModuleProfile()
    {
        CreateMap<Domain.Models.Modulo, ModuleResponseDTO>()
            .ForMember(dest => dest.Curso, opt => opt.MapFrom(src => new Curso
            {
                Id = src.Curso.Id,
                Name = src.Curso.Name
            }))
            .ForMember(dest => dest.Professor, opt => opt.MapFrom(src => new Professor
            {
                Id = src.Professor.Id,
                Name = src.Professor.Name
            }))
            .ForMember(dest => dest.Aulas, opt => opt.MapFrom(src =>
                src.Aulas.Select(a => new Aula
                {
                    Id = a.Id,
                    Theme = a.Theme
                }).ToList()
            ));

        // Mapeamento reverso (opcional)
        CreateMap<ModuleResponseDTO, Domain.Models.Modulo>()
            .ForMember(dest => dest.Curso, opt => opt.Ignore())       // evita conflitos se só o ID for necessário
            .ForMember(dest => dest.Professor, opt => opt.Ignore())   // idem acima
            .ForMember(dest => dest.Aulas, opt => opt.Ignore());      // idem

        CreateMap<CreateModuleDTO, Domain.Models.Modulo>()
            .ForMember(dest => dest.Id_Curso, opt => opt.MapFrom(src => src.Curso.Id))
            .ForMember(dest => dest.Id_Professor, opt => opt.MapFrom(src => src.Professor.Id))
            .ForMember(dest => dest.Curso, opt => opt.Ignore()) // 👈 impede AutoMapper de tentar mapear
            .ForMember(dest => dest.Professor, opt => opt.Ignore()) // idem, se necessário
            .ForMember(dest => dest.Aulas, opt => opt.MapFrom(src =>
                src.Aulas.Select(a => new Aula { Id = a.Id }).ToList()
            ));

        // Domain -> DTO (Retorno após criação ou edição, se necessário)
        CreateMap<Domain.Models.Modulo, CreateModuleDTO>()
            .ForMember(dest => dest.Curso, opt => opt.MapFrom(src => new CursoDTO
            {
                Id = src.Curso.Id,
            }))
            .ForMember(dest => dest.Professor, opt => opt.MapFrom(src => new ProfessorDTO
            {
                Id = src.Professor.Id
            }))
            .ForMember(dest => dest.Aulas, opt => opt.MapFrom(src =>
                src.Aulas.Select(a => new AulaDTO { Id = a.Id }).ToList()
            ));
    }
}
