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

public class GetTeacherByIdController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly UserManager<User> _userManager;

    public GetTeacherByIdController(ITeacherServices teacherService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _teacherService = teacherService;
    }

    [Authorize]
    [HttpGet("professores/{id:guid}")]
    public async Task<IActionResult> GetTeacherByIdAsync(Guid id)
    {
        var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);
        if (!autorizado)
            return resultado;

        var teacher = await _teacherService.GetTeacherByID(id);
        if (teacher == null)
            return NotFound(new { Message = "Professor não encontrado." });

        return Ok(teacher);
    }
}
