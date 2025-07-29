using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.RegistrationControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class DeleteRegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly IUserService _userService;
    public DeleteRegistrationController(IRegistrationService registrationService, IUserService userService)
    {
        _registrationService = registrationService;
        _userService = userService;
    }

    [HttpDelete("matricula/delete/{id:guid}")]
    public async Task<IActionResult> DeleteRegistrationAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deleteRegistration = await _registrationService.DeleteRegistrationAsync(id);
            return Ok(deleteRegistration);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao excluir matrícula: {message}");
        }
    }
}
