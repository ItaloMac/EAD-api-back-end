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
public class CreateAulaController : ControllerBase
{
    private readonly IAulasService _aulasService;
    private readonly IUserService _userService;

    public CreateAulaController(IAulasService aulasService, IUserService userService)
    {
        _aulasService = aulasService;
        _userService = userService;
    }

    [Authorize]
    [HttpPost("aulas/create")]
    public async Task<IActionResult> CreateAulaAsync([FromBody] CreateAulaDTO aula)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            if (aula == null)
                return BadRequest("Aula não pode ser nula.");

            var createdAula = await _aulasService.CreateAulaAsync(aula);
            return Ok(createdAula);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar a aula: {message}");
        }
    }
}
