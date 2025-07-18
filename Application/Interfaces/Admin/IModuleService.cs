using Application.DTOs.Admin.Module;

namespace Application.Interfaces.Admin;

public interface IModuleService
{
    Task<List<ModuleResponseDTO>> GetAllModulesAsync();
    Task<ModuleResponseDTO> GetModuleByIdAsync(Guid id);
}

