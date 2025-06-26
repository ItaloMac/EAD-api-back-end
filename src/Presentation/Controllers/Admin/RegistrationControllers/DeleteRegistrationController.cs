using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.RegistrationControllers;

[ApiController]
[Route("api/admin")]
public class DeleteRegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly UserManager<User> _userManager;
    public DeleteRegistrationController(IRegistrationService registrationService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _registrationService = registrationService;
    }

    [HttpDelete("matricula/delete/{id:guid}")]
    public async Task<IActionResult> DeleteRegistrationAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

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
