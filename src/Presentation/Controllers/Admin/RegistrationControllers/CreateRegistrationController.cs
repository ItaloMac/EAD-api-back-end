using System;
using Application.DTOs.Admin.Registration;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.RegistrationControllers;

[ApiController]
[Route("api/admin")]
public class CreateRegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly UserManager<User> _userManager;
    public CreateRegistrationController(IRegistrationService registrationService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _registrationService = registrationService;
    }

    [HttpPost("matriculas/create")]
    public async Task<IActionResult> CreateRegistrationAsync([FromBody] CreateRegistrationDTO responseDTO)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newRegistration = await _registrationService.PostRegistrationAsync(responseDTO);
            return Ok(newRegistration);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar matriculas do usuário: {message}");
        }
    }
}
