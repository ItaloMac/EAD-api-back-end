using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class CourseClassListController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly IUserService _userService;

    public CourseClassListController(ICourseServices courseServices, IUserService userService)
    {
        _courseServices = courseServices;
        _userService = userService;
    }

    [HttpGet("cursos/{CourseId:guid}/turmas")]
    public async Task<IActionResult> CourseClassListAsync(Guid CourseId)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listClass = await _courseServices.CourseClassListAssync(CourseId);
            if (listClass == null || !listClass.Any())
            {
                throw new ApplicationException("Não há turmas criadas para esse curso.");
            }
            return Ok(listClass);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar o curso: {message}");
        }
    }
}
