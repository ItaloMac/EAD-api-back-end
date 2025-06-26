using System;
using Application.DTOs.Admin.Registration;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.RegistrationControllers;

[ApiController]
[Route("api/admin")]
public class UpdateRegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly UserManager<User> _userManager;
    public UpdateRegistrationController(IRegistrationService registrationService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _registrationService = registrationService;
    }

    [HttpPut("matriculas/update/{id:guid}")]
    public async Task<IActionResult> UpdateRegistrationAsync(Guid id, [FromBody] UpdateRegistrationDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newDataRegistration = await _registrationService.PutRegistrationAsync(id, dto);

            return Ok(newDataRegistration);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualiza a matricula: {message}");
        }
    }
}
