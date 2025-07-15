using System;
using Application.Interfaces.Admin;
using Application.Services.Admin.TeacherService;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetAllCoursesTeacherController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly UserManager<User> _userManager;

    public GetAllCoursesTeacherController(ITeacherServices teacherService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _teacherService = teacherService;
    }

    [Authorize]
    [HttpGet("professores/{id:guid}/cursos")]
    public async Task<IActionResult> GetAllTeachersCourseAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listAllCourses = await _teacherService.GetCoursesByIdTeacherAsync(id);
            return Ok(listAllCourses);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar todos cursos do professor: {message}");
        }
    }
}
