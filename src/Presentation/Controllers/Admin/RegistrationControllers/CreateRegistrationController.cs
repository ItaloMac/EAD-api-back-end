using Application.DTOs.Admin.Registration;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.RegistrationControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class CreateRegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly IUserService _userService;
    public CreateRegistrationController(IRegistrationService registrationService, IUserService userService)
    {
        _registrationService = registrationService;
        _userService = userService;
    }

    [HttpPost("matriculas/create")]
    public async Task<IActionResult> CreateRegistrationAsync([FromBody] CreateRegistrationDTO responseDTO)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

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
