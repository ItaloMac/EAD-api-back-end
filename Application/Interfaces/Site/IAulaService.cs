using Application.DTOs;

namespace Application.Interfaces;

public interface IAulaService
{
    public Task<List<AulaDTO>> GetAllAulas();
}
