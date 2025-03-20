using System;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ProfessorService : IProfessorService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProfessorService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProfessorDTO>> GetProfessoresByIdCursoAsync(Guid cursoId)
    {
        var professores = await _context.CursoProfessores
        .Where(cp => cp.Id_Curso == cursoId) // Filtra pelo Id do curso
            .Select(cp => cp.Professor) // Seleciona os professores associados
            .Select(p => new ProfessorDTO // Mapeia para o DTO
            {
                Id = p.Id,
                Name = p.Name,
                MiniResume = p.MiniResume
            })
            .ToListAsync();

        return professores;
    }

    public async Task<List<ProfessorDTO>> GetAllProfessores()
    {
        var teachers = await _context.Professores.ToListAsync();
        return _mapper.Map<List<ProfessorDTO>>(teachers);
    }

    public Task<List<ProfessorDTO>> GetByIdProfessores()
    {
        throw new NotImplementedException();
    }
}
