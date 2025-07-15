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

public class DeleteTeacherController : ControllerBase
{
    private readonly ITeacherServices _teacherService;
    private readonly UserManager<User> _userManager;

    public DeleteTeacherController(ITeacherServices teacherService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _teacherService = teacherService;
    }

    [Authorize]
    [HttpDelete("professores/delete/{id:guid}")]
    public async Task<IActionResult> DeleteTeacherAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deleteTeacher = await _teacherService.DeleteTeacherAsync(id);
            return Ok(deleteTeacher);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao excluir professor: {message}");
        }
    }
}
