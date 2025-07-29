using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.UserControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetUserById : ControllerBase
{
    private readonly IUserService _userService;
    public GetUserById(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("usuarios/{id:guid}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);
        if (!autorizado)
            return resultado;

        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound(new { Message = "Usuário não encontrado." });

        return Ok(user);
    }
}
