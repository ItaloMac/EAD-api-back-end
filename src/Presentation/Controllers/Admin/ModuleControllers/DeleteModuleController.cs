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
public class DeleteModuleController : ControllerBase
{
    private readonly IModuleService _moduleService;
    private readonly IUserService _userService;

    public DeleteModuleController(IModuleService moduleService, IUserService userService)
    {
        _moduleService = moduleService;
        _userService = userService;
    }

    [HttpDelete("modulos/{id:guid}/delete")]
    public async Task<IActionResult> DeleteModuleAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var deletedModule = await _moduleService.DeleteModuleAsync(id);

            return Ok("Módulo deletado com sucesso.");
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao deletar módulo: {message}");
        }
    }
}
