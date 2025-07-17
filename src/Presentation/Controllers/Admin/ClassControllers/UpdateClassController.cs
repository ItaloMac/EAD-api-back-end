using System;
using Application.DTOs.Admin.Class;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ClassControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class UpdateClassController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly UserManager<User> _userManager;

    public UpdateClassController(IClassServices classService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _classService = classService;
    }

    [Authorize]
    [HttpPut("turmas/{id:guid}/update")]
     public async Task<IActionResult> UpdateClassAsync(Guid id, CreateClassDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var updateClass = await _classService.UpdateClassAsync(id, dto);
            return Ok(updateClass);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualizar a turma: {message}");
        }
    }
}
