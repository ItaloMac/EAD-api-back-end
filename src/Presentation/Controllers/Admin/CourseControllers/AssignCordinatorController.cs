using Application.DTOs.Admin.Course;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class AssignCordinatorController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly IUserService _userService;
    public AssignCordinatorController(ICourseServices courseServices, IUserService userService)
    {
        _courseServices = courseServices;
        _userService = userService;
    }

    [HttpPost("cursos/{CourseId:guid}/coordenador")]
    public async Task<IActionResult> AssignTeacherToCourseAsync(Guid CourseId, [FromBody] AssignCordinatorDTO cordinatorDTO)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var addCordinator = await _courseServices.AssignCordinatorAsync(CourseId, cordinatorDTO);
            return Ok(addCordinator);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao adicionar o coordenador: {message}");
        }
    }
}
