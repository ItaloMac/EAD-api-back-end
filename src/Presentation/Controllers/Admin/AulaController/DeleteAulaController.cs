using System;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.AulaController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class DeleteAulaController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly UserManager<User> _userManager;

    public DeleteAulaController(IAulasService aulasService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _aulasService = aulasService;
    }

    [Authorize]
    [HttpDelete("aulas/{id:guid}/delete")]
    public async Task<IActionResult> DeleteAulaAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deletedAula = await _aulasService.DeleteAulaAsync(id);
            return Ok(deletedAula);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao deletar a aula: {message}");
        }
    }
}
