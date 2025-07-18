using System;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ModuleControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class GetModuleByIdController : ControllerBase
{
    private readonly IModuleService _moduleService;
    private readonly UserManager<User> _userManager;

    public GetModuleByIdController(IModuleService moduleService, UserManager<User> userManager)
    {
        _moduleService = moduleService;
        _userManager = userManager;
    }

    [HttpGet("modulos/{id:guid}")]
    public async Task<IActionResult> GetAllModulesAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
                return NotFound($"Módulo com ID {module} não encontrado.");

            return Ok(module);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao obter módulo: {message}");
        }
     }
}
