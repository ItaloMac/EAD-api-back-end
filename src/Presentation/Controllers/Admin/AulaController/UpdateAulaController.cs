using System;
using Application.DTOs.Admin.Aula;
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
public class UpdateAulaController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly UserManager<User> _userManager;

    public UpdateAulaController(IAulasService aulasService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _aulasService = aulasService;
    }

    [Authorize]
    [HttpPut("aulas/{id:guid}/update")]
    public async Task<IActionResult> UpdateAulaAsync(Guid id, [FromBody] CreateAulaDTO aula)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var updatedAula = await _aulasService.UpdateAulaAsync(id,aula);
            if (updatedAula == null)
                return NotFound("Aula não encontrada.");

            return Ok(updatedAula);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualizar a aula: {message}");
        }
    }
}
