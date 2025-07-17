using Application.DTOs.Admin.Class;

namespace Application.Interfaces.Admin;

public interface IClassServices
{
    Task<List<ClassResponseDTO>> GetAllClassesAsync();
    Task<ClassResponseDTO> GetClassById(Guid id);
    Task<CreateClassDTO> CreateClassAsync(CreateClassDTO dto);
}
