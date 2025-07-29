using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ModuleControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetModuleByIdController : ControllerBase
{
    private readonly IModuleService _moduleService;
    private readonly IUserService _userService;

    public GetModuleByIdController(IModuleService moduleService, IUserService userService)
    {
        _moduleService = moduleService;
        _userService = userService;
    }

    [HttpGet("modulos/{id:guid}")]
    public async Task<IActionResult> GetAllModulesAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
                return NotFound($"Módulo com ID {module} não encontrado.");

            return Ok(module);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao obter módulo: {message}");
        }
     }
}
