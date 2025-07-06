using System;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetAllRegistrationsController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly UserManager<User> _userManager;
    public GetAllRegistrationsController(IRegistrationService registrationService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _registrationService = registrationService;
    }

    [HttpGet("matriculas")]
    public async Task<IActionResult> GetAllRegistrationsAsync()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;


            var allRegistrations = await _registrationService.GetAllRegistrations();

            return Ok(allRegistrations);
        }
        catch (Exception ex)
        {
             var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao buscar as matriculas do usuário: {message}");
        }
    }

   
}
