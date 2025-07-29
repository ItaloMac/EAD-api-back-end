using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.RegistrationControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetRegistrationByIdController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly IUserService _userService;
    public GetRegistrationByIdController(IRegistrationService registrationService, IUserService userService)
    {
        _registrationService = registrationService;
        _userService = userService;
    }

    [HttpGet("matricula/{id:guid}")]
    public async Task<IActionResult> GetRegistrationByIdAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var registration = await _registrationService.GetRegistrationById(id);

            return Ok(registration);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao buscar as matriculas do usuário: {message}");
        }
    }
}
