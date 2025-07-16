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

public class GetClassByIdController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly UserManager<User> _userManager;

    public GetClassByIdController(IClassServices classService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _classService = classService;
    }

    [Authorize]
    [HttpGet("turmas/{id:guid}")]
     public async Task<IActionResult> GetClassByIdAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var listClass= await _classService.GetClassById(id);
            return Ok(listClass);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao listar a turma: {message}");
        }
    }
}
