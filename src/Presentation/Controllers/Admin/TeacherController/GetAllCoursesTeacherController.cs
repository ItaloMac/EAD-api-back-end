using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllCoursesTeacherController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly IUserService _userService;

    public GetAllCoursesTeacherController(ITeacherServices teacherService, IUserService userService)
    {
        _userService = userService;
        _teacherService = teacherService;
    }

    [Authorize]
    [HttpGet("professores/{id:guid}/cursos")]
    public async Task<IActionResult> GetAllTeachersCourseAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listAllCourses = await _teacherService.GetCoursesByIdTeacherAsync(id);
            return Ok(listAllCourses);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar todos cursos do professor: {message}");
        }
    }
}
