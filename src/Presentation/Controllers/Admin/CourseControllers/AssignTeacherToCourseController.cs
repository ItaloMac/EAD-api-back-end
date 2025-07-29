using Application.DTOs.Admin.Course;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class AssignTeacherToCourseController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly IUserService _userService;

    public AssignTeacherToCourseController(ICourseServices courseServices, IUserService userService)
    {
        _courseServices = courseServices;
        _userService = userService;
    }

    [HttpPost("cursos/adicionar-professor/{CourseId:guid}")]
    public async Task<IActionResult> AssignTeacherToCourseAsync(Guid CourseId, [FromBody] CourseTeacherDTO ProfessorId)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var addTeacher = await _courseServices.AssignProfessorToCourseAsync(CourseId, ProfessorId);
            return Ok(addTeacher);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar o curso: {message}");
        }
    }

}
