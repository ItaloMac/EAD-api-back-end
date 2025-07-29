using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllModulesTeacherController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly IUserService _userService;

    public GetAllModulesTeacherController(ITeacherServices teacherService, IUserService userService)
    {
        _teacherService = teacherService;
        _userService = userService;
    }
 
    [Authorize]
    [HttpGet("professores/{id:guid}/modulos")]
    public async Task<IActionResult> GetAllModulesCourseAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listAllModules = await _teacherService.GetModulesByIdTeacherAsync(id);
            return Ok(listAllModules);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar todos modulos do professor: {message}");
        }
    }
}
