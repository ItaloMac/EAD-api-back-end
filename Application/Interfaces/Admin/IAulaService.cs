using Application.DTOs.Admin.Aula;

namespace Application.Interfaces.Admin;

public interface IAulaService
{
    Task<AulaResponseDTO> GetAllAulasAsync();
}
