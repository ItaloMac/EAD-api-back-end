using Application.DTOs.Admin.Teacher;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class CreateTeacherController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly IUserService _userService;
    public CreateTeacherController(ITeacherServices teacherService, IUserService userService)
    {
        _teacherService = teacherService;
        _userService = userService;
    }

    [Authorize]
    [HttpPost("professores/create")]
    public async Task<IActionResult> CreateTeacherAsync([FromBody] CreateTeacherDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newTeacher = await _teacherService.CreateTeacherAsync(dto);

            return Ok(newTeacher);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
