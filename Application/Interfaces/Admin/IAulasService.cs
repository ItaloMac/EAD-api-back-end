using Application.DTOs.Admin.Aula;

namespace Application.Interfaces.Admin;

public interface IAulasService
{
    Task<List<AulaResponseDTO>> GetAllAulasAsync();
    Task<AulaResponseDTO> GetAulaByIdAsync(Guid id);
    Task<CreateAulaDTO> CreateAulaAsync(CreateAulaDTO createAulaDTO);
    Task<UpdateAulaDTO> UpdateAulaAsync(Guid id, UpdateAulaDTO updateAulaDTO);
    Task<bool> DeleteAulaAsync(Guid id);
}
