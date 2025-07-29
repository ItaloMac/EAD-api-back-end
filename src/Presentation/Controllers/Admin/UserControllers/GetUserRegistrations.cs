using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.UserControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetUserRegistrationsController : ControllerBase
{
    private readonly IUserService _userService;
    public GetUserRegistrationsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("usuarios/{id:guid}/matriculas")]

    public async Task<IActionResult> GetUserRegistrationsByUserIdAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

        var registrations = await _userService.GetUserRegistrationsByUserIdAsync(id);
            return Ok(registrations);
        }
        catch (Exception ex)
        {
             var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao buscar as matriculas do usuário: {message}");
        }
    }
}
