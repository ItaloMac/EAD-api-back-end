using System;
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
public class DeleteClassController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly UserManager<User> _userManager;

    public DeleteClassController(IClassServices classService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _classService = classService;
    }

    [Authorize]
    [HttpDelete("turmas/{id:guid}/delete")]
    public async Task<IActionResult> DeleteClassAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            await _classService.DeleteClassAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao deletar a turma: {message}");
        }
    }
}
