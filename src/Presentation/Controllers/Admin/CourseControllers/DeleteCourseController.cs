using System;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.CourseControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class DeleteCourseController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly UserManager<User> _userManager;
    public DeleteCourseController(ICourseServices courseServices, UserManager<User> userManager)
    {
        _userManager = userManager;
        _courseServices = courseServices;
    }

    [HttpDelete("cursos/delete/{id:guid}")]
    [ApiExplorerSettings(GroupName = "Portal Admin")]
    public async Task<IActionResult> DeleteCourseAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deleteToCourse = await _courseServices.DeleteCourseAsync(id);

            return Ok(deleteToCourse);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao deletar o curso: {message}");
        }
    }
}
