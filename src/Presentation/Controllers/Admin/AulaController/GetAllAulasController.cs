using System;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.AulaController;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetAllAulasController : ControllerBase
{
    private readonly IAulaService _aulaService;
    private readonly UserManager<User> _userManager;

    public GetAllAulasController(IAulaService aulaService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _aulaService = aulaService;
    }

    [Authorize]
    [HttpGet("aulas")]
    public async Task<IActionResult> GetAllAulasAsync()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var aulas = await _aulaService.GetAllAulas();
            return Ok(aulas);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao obter as aulas: {message}");
        }
    }
}
