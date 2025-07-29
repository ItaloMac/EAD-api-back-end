using Application.DTOs.Admin.Course;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class CreateCourseController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly IUserService _userService;
    public CreateCourseController(ICourseServices courseServices, IUserService userService)
    {
        _courseServices = courseServices;
        _userService = userService;
    }

    [HttpPost("cursos/create")]
    public async Task<IActionResult> CreateCourseAsync([FromBody] CreateCourseDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newCourse = await _courseServices.CreateCourseAsync(dto);
            return Ok(newCourse);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar o curso: {message}");
        }
    }
}
