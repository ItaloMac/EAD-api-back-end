using Application.DTOs.Admin.Aula;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.AulaController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class UpdateAulaController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly IUserService _userService;

    public UpdateAulaController(IAulasService aulasService, IUserService userService)
    {
        _aulasService = aulasService;
        _userService = userService;
    }

    [Authorize]
    [HttpPut("aulas/{id:guid}/update")]
    public async Task<IActionResult> UpdateAulaAsync(Guid id, [FromBody] UpdateAulaDTO aula)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var updatedAula = await _aulasService.UpdateAulaAsync(id,aula);
            if (updatedAula == null)
                return NotFound("Aula não encontrada.");

            return Ok(updatedAula);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualizar a aula: {message}");
        }
    }
}
