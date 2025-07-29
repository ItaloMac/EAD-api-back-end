using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.AulaController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class DeleteAulaController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly IUserService _userService;

    public DeleteAulaController(IAulasService aulasService, IUserService userService)
    {
        _aulasService = aulasService;
        _userService = userService;
    }

    [Authorize]
    [HttpDelete("aulas/{id:guid}/delete")]
    public async Task<IActionResult> DeleteAulaAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deletedAula = await _aulasService.DeleteAulaAsync(id);
            return Ok(deletedAula);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao deletar a aula: {message}");
        }
    }
}
