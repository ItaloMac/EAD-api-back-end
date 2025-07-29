using Application.DTOs.Admin.Teacher;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class UpdateTeacherController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly IUserService _userService;

    public UpdateTeacherController(ITeacherServices teacherService, IUserService userService)
    {
        _teacherService = teacherService;
        _userService = userService;
    }

    [Authorize]
    [HttpPut("professores/update/{id:guid}")]
    public async Task<IActionResult> UpdateTeacherAsync(Guid id, [FromBody] UpdateTeacherDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newDataTeacher = await _teacherService.UpdateTeacherAsync(id, dto);

            return Ok(newDataTeacher);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualiza o professor: {message}");
        }
    }
}
