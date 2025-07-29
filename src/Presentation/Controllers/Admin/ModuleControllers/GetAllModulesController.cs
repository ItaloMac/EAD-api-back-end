using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ModuleControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllModulesController : ControllerBase
{
    private readonly IModuleService _moduleService;
    private readonly IUserService _userService;

    public GetAllModulesController(IModuleService moduleService, IUserService userService)
    {
        _moduleService = moduleService;
        _userService = userService;
    }

    [HttpGet("modulos")]
    public async Task<IActionResult> GetAllModulesAsync()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var modules = await _moduleService.GetAllModulesAsync();
            return Ok(modules);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao obter módulos: {message}");
        }
    }
}
