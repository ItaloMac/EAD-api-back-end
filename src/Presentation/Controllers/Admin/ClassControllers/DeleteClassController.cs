using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ClassControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class DeleteClassController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly IUserService _userService;

    public DeleteClassController(IClassServices classService, IUserService userService)
    {
        _classService = classService;
        _userService = userService;
    }

    [Authorize]
    [HttpDelete("turmas/{id:guid}/delete")]
    public async Task<IActionResult> DeleteClassAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            await _classService.DeleteClassAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao deletar a turma: {message}");
        }
    }
}
