using Application.DTOs.Admin.Registration;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.RegistrationControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class UpdateRegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly IUserService _userService;
    public UpdateRegistrationController(IRegistrationService registrationService, IUserService userService)
    {
        _registrationService = registrationService;
        _userService = userService;
    }

    [HttpPut("matriculas/update/{id:guid}")]
    public async Task<IActionResult> UpdateRegistrationAsync(Guid id, [FromBody] UpdateRegistrationDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

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
