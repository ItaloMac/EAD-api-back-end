using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllTeachersController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly IUserService _userService;

    public GetAllTeachersController(ITeacherServices teacherService, IUserService userService)
    {
        _teacherService = teacherService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("professores")]
    public async Task<IActionResult> GetAllTeachers()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var allTeachers = await _teacherService.GetAllTeachers();
            if (allTeachers == null || !allTeachers.Any())
            {
                return NotFound("Nenhum professor cadastrado.");
            }

            return Ok(allTeachers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
