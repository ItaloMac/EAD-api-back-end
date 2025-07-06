using Application.DTOs.Admin.Course;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class CourseClassListController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly UserManager<User> _userManager;
    public CourseClassListController(ICourseServices courseServices, UserManager<User> userManager)
    {
        _userManager = userManager;
        _courseServices = courseServices;
    }

    [HttpGet("cursos/{CourseId:guid}/turmas")]
    public async Task<IActionResult> CourseClassListAsync(Guid CourseId)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

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
