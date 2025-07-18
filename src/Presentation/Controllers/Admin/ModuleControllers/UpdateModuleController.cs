using System;
using Application.DTOs.Admin.Module;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ModuleControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class UpdateModuleController : ControllerBase
{
    private readonly IModuleService _moduleService;
    private readonly UserManager<User> _userManager;

    public UpdateModuleController(IModuleService moduleService, UserManager<User> userManager)
    {
        _moduleService = moduleService;
        _userManager = userManager;
    }

    [HttpPut("modulos/{id:guid}/update")]
    public async Task<IActionResult> UpdateModuleAsync(Guid id, [FromBody] CreateModuleDTO module)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var updatedModule = await _moduleService.UpdateModuleAsync(id,module);
            if (updatedModule == null)
                return NotFound($"Módulo com ID {id} não encontrado.");

            return Ok(updatedModule);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualizar módulo: {message}");
        }
    }
}
