using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetAllTeachersCourseController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly UserManager<User> _userManager;
    public GetAllTeachersCourseController(ICourseServices courseServices, UserManager<User> userManager)
    {
        _userManager = userManager;
        _courseServices = courseServices;
    }

    [HttpGet("cursos/{CourseId:guid}/professores")]
    public async Task<IActionResult> GetAllTeachersCourseAsync(Guid CourseId)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listAllTeachers = await _courseServices.GetTeachersByIdCourseAsync(CourseId);
            return Ok(listAllTeachers);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar todos professores do curso: {message}");
        }
    }
}
