using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.AspNetCore.Authorization; // Seu modelo de usuário
using Microsoft.Extensions.Configuration;


[ApiController]
[Route("api/[controller]")]
public class ConfirmEmailController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public ConfirmEmailController(UserManager<User> userManager, IConfiguration configuratio)
    {
        _userManager = userManager;
        _configuration = configuration;

    }

    [AllowAnonymous]
    [HttpGet("confirm")]
    public async Task<IActionResult> Confirm(string userId, string code)
    {
        var frontendUrl = _configuration["Frontend:BaseUrl"];

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            return Redirect($"{frontendUrl}/confirmacao-email?status=erro&msg=Dados inválidos");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Redirect($"{frontendUrl}/confirmacao-email?status=erro&msg=Usuário não encontrado");

        var decodedCode = code.Replace(' ', '+');
        var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

        if (result.Succeeded)
            return Redirect($"{frontendUrl}/confirmacao-email?status=sucesso");

        var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
        return Redirect($"{frontendUrl}/confirmacao-email?status=erro&msg={Uri.EscapeDataString(errorMessage)}");
    }
}