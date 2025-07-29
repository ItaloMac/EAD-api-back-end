using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ClassControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetAllClassesController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly IUserService _userService;

    public GetAllClassesController(IClassServices classService, IUserService userService)
    {
        _classService = classService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("turmas/")]
    public async Task<IActionResult> GetAllClassesAsync()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listAllClasses = await _classService.GetAllClassesAsync();
            return Ok(listAllClasses);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar todos as turmas: {message}");
        }
    }
}
