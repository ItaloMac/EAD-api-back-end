using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ModuleControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class AssignTeacherToModuleController : ControllerBase
{
    private readonly IModuleService _moduleService;
    private readonly IUserService _userService;

    public AssignTeacherToModuleController(IModuleService moduleService, IUserService userService)
    {
        _moduleService = moduleService;
        _userService = userService;
    }

    [HttpPost("modulos/{moduleId:guid}/professor/{teacherId:guid}")]
    public async Task<IActionResult> AssignTeacherToModule(Guid moduleId, Guid teacherId)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var result = await _moduleService.AssignTeacherToModuleAsync(moduleId, teacherId);
            if (result == null)
            {
                return NotFound("Modulo ou professor não encontrado.");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao atribuir o professor ao modulo: " + ex.Message);
        }
    }
}
