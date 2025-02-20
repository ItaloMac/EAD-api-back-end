using Application.Interfaces;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Application.Services;

public class CursoService : ICursoService
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;
    
    public CursoService(IApplicationDbContext context, IMapper mapper) //constructor
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CursoDTO?> GetCursoByIdAsync(Guid id)
    {
        var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
        return curso == null ? null : _mapper.Map<CursoDTO>(curso);
    }

    public async Task<List<CursoDTO>> GetCursosAsync()
    {
        var cursos = await _context.Cursos.ToListAsync();
        return _mapper.Map<List<CursoDTO>>(cursos);
    }
}
