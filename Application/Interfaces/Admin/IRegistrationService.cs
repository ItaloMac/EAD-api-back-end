using System;
using Application.DTOs.Admin.Registration;

namespace Application.Interfaces.Admin;

public interface IRegistrationService
{
    Task<List<RegistrationResponseDTO>> GetAllRegistrations();
    Task<RegistrationResponseDTO> GetRegistrationById(Guid id);

}
