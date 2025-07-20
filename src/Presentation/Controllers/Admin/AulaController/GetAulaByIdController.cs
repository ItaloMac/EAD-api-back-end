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

public class GetAulaByIdController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly UserManager<User> _userManager;

    public GetAulaByIdController(IAulasService aulasService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _aulasService = aulasService;
    }

    [Authorize]
    [HttpGet("aulas/{id:guid}")]
    public async Task<IActionResult> GetAulaByIdAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var aula = await _aulasService.GetAulaByIdAsync(id);
            if (aula == null)
                return NotFound($"Aula with ID {id} not found.");

            return Ok(aula);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao obter a aula: {message}");
        }
    }
}
