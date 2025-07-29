using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllCoursesController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly IUserService _userService;
    public GetAllCoursesController(ICourseServices courseServices, IUserService userService)
    {
        _courseServices = courseServices;
        _userService = userService;
    }

    [HttpGet("cursos")]
    public async Task<IActionResult> GetAllCoursesAsync()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listAllCourses = await _courseServices.GetAllCoursesAsync();
            return Ok(listAllCourses);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar todos os cursos: {message}");
        }
    }
}
