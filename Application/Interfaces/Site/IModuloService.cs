namespace Application.Interfaces;

public interface IModuloService
{
    public Task<List<ModuloDTO>> GetAllModulo();
    public Task<List<ModuloDTO>> GetByIdModulo();
    public Task<List<ModuloDTO>> GetModuloByIdCursoAsync(Guid cursoId);
}
