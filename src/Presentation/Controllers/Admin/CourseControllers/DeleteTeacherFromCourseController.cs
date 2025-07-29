using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class DeleteTeacherFromCourseController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly IUserService _userService;
    public DeleteTeacherFromCourseController(ICourseServices courseServices, IUserService userService)
    {
        _courseServices = courseServices;
        _userService = userService;
    }

    [HttpDelete("cursos/delete-teacher/{CourseId:guid}/{ProfessorId:guid}")]
    [ApiExplorerSettings(GroupName = "Portal Admin")]
    public async Task<IActionResult> DeleteTeacherFromCourseAsync(Guid CourseId, Guid ProfessorId)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deleteTeacher = await _courseServices.DeleteTeacherFromCourseAsync(CourseId, ProfessorId);

            return Ok(deleteTeacher);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao remover o professor do curso: {message}");
        }
    }
}
