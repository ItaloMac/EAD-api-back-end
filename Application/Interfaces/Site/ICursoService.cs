using Application.DTOs;

namespace Application.Interfaces;

public interface ICursoService
{
    Task<List<CursoDTO>> GetCursosAsync();
    Task<CursoDTO?>GetCursoByIdAsync(Guid id);
};
