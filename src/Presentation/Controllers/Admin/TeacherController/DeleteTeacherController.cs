using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class DeleteTeacherController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly IUserService _userService;

    public DeleteTeacherController(ITeacherServices teacherService, IUserService userService)
    {
        _teacherService = teacherService;
        _userService = userService;
    }

    [Authorize]
    [HttpDelete("professores/delete/{id:guid}")]
    public async Task<IActionResult> DeleteTeacherAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deleteTeacher = await _teacherService.DeleteTeacherAsync(id);
            return Ok(deleteTeacher);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao excluir professor: {message}");
        }
    }
}
