using Application.DTOs.Admin.Class;

namespace Application.Interfaces.Admin;

public interface IClassServices
{
    Task<List<ClassResponseDTO>> GetAllClassesAsync();
}
