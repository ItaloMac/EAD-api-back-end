using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllRegistrationsController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly IUserService _userService;
    public GetAllRegistrationsController(IRegistrationService registrationService, IUserService userService)
    {
        _registrationService = registrationService;
        _userService = userService;
    }

    [HttpGet("matriculas")]
    public async Task<IActionResult> GetAllRegistrationsAsync()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

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
