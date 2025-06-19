using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

public class AuthAdmin
{
    private readonly UserManager<User> _userManager;

    public AuthAdmin(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(bool autorizado, IActionResult resultado)> ValidarAdminAsync(ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return (false, new UnauthorizedObjectResult("Usuário não autenticado."));

        var user = await _userManager.FindByIdAsync(userId);
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
