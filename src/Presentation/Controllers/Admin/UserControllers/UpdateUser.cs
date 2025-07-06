using Application.DTOs.Admin.User;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.UserControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class UpdateUserController : Controller
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public UpdateUserController (IUserService userService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _userService = userService;
    }

    [HttpPut("usuarios/update/{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDTO user)
    {

        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var updatedUser = await _userService.UpdateUserAsync(id, user);
            if (updatedUser == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualizar usuário: {message}");
        }
    }

}
