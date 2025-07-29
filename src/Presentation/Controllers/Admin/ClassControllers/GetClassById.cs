using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ClassControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetClassByIdController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly IUserService _userService;

    public GetClassByIdController(IClassServices classService, IUserService userService)
    {
        _classService = classService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("turmas/{id:guid}")]
     public async Task<IActionResult> GetClassByIdAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listClass= await _classService.GetClassById(id);
            return Ok(listClass);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar a turma: {message}");
        }
    }
}
