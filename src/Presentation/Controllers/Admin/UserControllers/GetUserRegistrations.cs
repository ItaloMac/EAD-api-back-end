using Application.Interfaces.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.UserControllers;

[ApiController]
[Route("api/admin")]

public class GetUserRegistrationsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    public GetUserRegistrationsController(IUserService userService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _userService = userService;
    }

    [HttpGet("usuarios/{id:guid}/matriculas")]

    public async Task<IActionResult> GetUserRegistrationsByUserIdAsync(Guid id)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userManager).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;


            var registrations = await _userService.GetUserRegistrationsByUserIdAsync(id);
            return Ok(registrations);
        }
        catch (Exception ex)
        {
             var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao buscar as matriculas do usuário: {message}");
        }
    }

}
