using Application.DTOs.Admin.Module;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ModuleControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class CreateModuleController : ControllerBase
{
    private readonly IModuleService _moduleService;
    private readonly IUserService _userService;

    public CreateModuleController(IModuleService moduleService, IUserService userService)
    {
        _moduleService = moduleService;
        _userService = userService;
    }

    [HttpPost("modulos/create")]
    public async Task<IActionResult> CreateModuleAsync([FromBody] CreateModuleDTO module)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            if (module == null)
                return BadRequest("Módulo inválido.");

            var createdModule = await _moduleService.CreateModuleAsync(module);
            return Ok(createdModule);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar módulo: {message}");
        }
    }
}
