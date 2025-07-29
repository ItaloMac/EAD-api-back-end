using Application.DTOs.Admin.Course;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class UpdateCourseController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly IUserService _userService;
    public UpdateCourseController(ICourseServices courseServices, IUserService userService)
    {
        _courseServices = courseServices;
        _userService = userService;
    }

    [HttpPut("cursos/update/{id:guid}")]
    [ApiExplorerSettings(GroupName = "Portal Admin")]
    public async Task<IActionResult> UpdateCourseAsync(Guid id, [FromBody] UpdateCourseDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newDataCourse = await _courseServices.UpdateCourseAsync(id, dto);

            return Ok(newDataCourse);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualiza o curso: {message}");
        }
    }
}
