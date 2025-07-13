using System;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.TeacherController;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetAllTeachersController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly UserManager<User> _userManager;

    public GetAllTeachersController(ITeacherServices teacherService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _teacherService = teacherService;
    }

    [Authorize]
    [HttpGet("professores")]

    public async Task<IActionResult> GetAllTeachers()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var allTeachers = await _teacherService.GetAllTeachers();
            if (allTeachers == null || !allTeachers.Any())
            {
                return NotFound("Nenhum professor cadastrado.");
            }

            return Ok(allTeachers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
