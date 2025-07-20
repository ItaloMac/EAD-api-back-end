using Application.DTOs.Admin.Aula;
using Application.DTOs.Admin.Module;

namespace Application.Interfaces.Admin;

public interface IAulasService
{
    Task<List<AulaResponseDTO>> GetAllAulasAsync();
    Task<AulaResponseDTO> GetAulaByIdAsync(Guid id);
    Task<CreateAulaDTO> CreateAulaAsync(CreateAulaDTO createAulaDTO);
    Task<CreateAulaDTO> UpdateAulaAsync(Guid id, CreateAulaDTO updateAulaDTO);
}
