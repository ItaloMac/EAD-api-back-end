using System.Security.Claims;
using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.UserControllers;

[ApiController]
[Route("api/admin")]

public class GetAllUsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public GetAllUsersController(IUserService userService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("usuarios")]

    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var users = await _userService.GetAllUsers();
            if (users == null || !users.Any())
            {
                return NotFound("Nenhum usuário cadastrado.");
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
