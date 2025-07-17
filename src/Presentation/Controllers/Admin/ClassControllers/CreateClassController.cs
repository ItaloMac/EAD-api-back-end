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
public class CreateClassController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly UserManager<User> _userManager;

    public CreateClassController(IClassServices classService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _classService = classService;
    }

    [Authorize]
    [HttpPost("turmas/create")]
    public async Task<IActionResult> CreateClassAsync([FromBody] CreateClassDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newClass = await _classService.CreateClassAsync(dto);
            return Ok(newClass);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar a turma: {message}");
        }
    }
}
