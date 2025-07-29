using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.AulaController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAulaByIdController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly IUserService _userService;

    public GetAulaByIdController(IAulasService aulasService, IUserService userService)
    {
        _aulasService = aulasService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("aulas/{id:guid}")]
    public async Task<IActionResult> GetAulaByIdAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var aula = await _aulasService.GetAulaByIdAsync(id);
            if (aula == null)
                return NotFound($"Aula with ID {id} not found.");

            return Ok(aula);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao obter a aula: {message}");
        }
    }
}
