using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetTeacherByIdController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly IUserService _userService;

    public GetTeacherByIdController(ITeacherServices teacherService, IUserService userService)
    {
        _teacherService = teacherService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("professores/{id:guid}")]
    public async Task<IActionResult> GetTeacherByIdAsync(Guid id)
    {
        var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);
        if (!autorizado)
            return resultado;

        var teacher = await _teacherService.GetTeacherByID(id);
        if (teacher == null)
            return NotFound(new { Message = "Professor não encontrado." });

        return Ok(teacher);
    }
}
