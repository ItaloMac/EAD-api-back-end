using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.AulaController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllAulasController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly IUserService _userService;

    public GetAllAulasController(IAulasService aulasService, IUserService userService)
    {
        _aulasService = aulasService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("aulas")]
    public async Task<IActionResult> GetAllAulasAsync()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var aulas = await _aulasService.GetAllAulasAsync();
            return Ok(aulas);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao obter as aulas: {message}");
        }
    }
}
