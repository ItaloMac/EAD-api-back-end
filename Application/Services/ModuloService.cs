using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Application.Services;

public class ModuloService : IModuloService
{
    public required IApplicationDbContext _context;
    public required IMapper _mapper;

    public ModuloService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<ModuloDTO>> GetAllModulo()
    {
        throw new NotImplementedException();
    }

    public Task<List<ModuloDTO>> GetByIdModulo()
    {
        throw new NotImplementedException();
    }

    public async Task<List<ModuloDTO>> GetModuloByIdCursoAsync(Guid cursoId)
    {
        var response = await _context.Modulos
        .Where(cm => cm.Id_Curso == cursoId)
        .Select(m => new ModuloDTO // Mapeia para o DTO
            {
                Id = m.Id,
                Theme = m.Theme,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                WorkLoad = m.WorkLoad,
                Id_Curso = m.Id_Curso,
                Id_Professor = m.Id_Professor,
                Ids_Aulas = m.Aulas.Select(a => a.Id).ToList()     
            })
            .ToListAsync();
        return response;
    }
}
