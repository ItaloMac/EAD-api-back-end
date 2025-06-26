using Application.DTOs.Admin.Registration;
using Domain.Models;

namespace Application.Interfaces.Admin;

public interface IRegistrationService
{
    Task<List<RegistrationResponseDTO>> GetAllRegistrations();
    Task<RegistrationResponseDTO> GetRegistrationById(Guid id);
    Task<CreateRegistrationDTO> PostRegistrationAsync(CreateRegistrationDTO registrationDTO);
    Task<UpdateRegistrationDTO> PutRegistrationAsync(Guid Id, UpdateRegistrationDTO registrationDTO);
    Task<bool> DeleteRegistrationAsync(Guid Id);
}
