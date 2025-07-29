using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Infrastucture;
using Application.Interfaces.Admin;

public class AuthAdmin
{
    private readonly IUserService _userService;

    public AuthAdmin(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<(bool autorizado, IActionResult resultado)> ValidarAdminAsync(ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine($"User ID Auth: {userId}");
        Console.WriteLine(Guid.Parse(userId ?? string.Empty));

        if (userId == null)
            return (false, new UnauthorizedObjectResult("Usuário não autenticado."));

        var user = await _userService.GetUserById(Guid.Parse(userId));
        if (user == null)
            return (false, new NotFoundObjectResult("Usuário não encontrado."));

        if (user.UserType != UserType.Administrador)
            return (false, new ObjectResult("Acesso negado. Apenas administradores podem acessar esse recurso.")
            {
                StatusCode = 403
            });

        return (true, null)!;
    }
}
