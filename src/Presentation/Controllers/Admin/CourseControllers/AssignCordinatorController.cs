using System;
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
public class AssignCordinatorController : ControllerBase
{
    private readonly ICourseServices _courseServices;
    private readonly UserManager<User> _userManager;
    public AssignCordinatorController(ICourseServices courseServices, UserManager<User> userManager)
    {
        _userManager = userManager;
        _courseServices = courseServices;
    }

    [HttpPost("cursos/{CourseId:guid}/coordenador")]
    public async Task<IActionResult> AssignTeacherToCourseAsync(Guid CourseId, [FromBody] AssignCordinatorDTO cordinatorDTO)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

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
