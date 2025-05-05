using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.AspNetCore.Authorization; // Seu modelo de usuário

[ApiController]
[Route("api/[controller]")]
public class ConfirmEmailController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public ConfirmEmailController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpGet("confirm")]
    public async Task<IActionResult> Confirm(string userId, string code) // Método renomeado
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            return BadRequest("UserId e Code são obrigatórios");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("Usuário não encontrado");

        var decodedCode = code.Replace(' ', '+');
        var result = await _userManager.ConfirmEmailAsync(user, decodedCode);
        
        return result.Succeeded
            ? Ok(new { Success = true, Message = "E-mail confirmado!" })
            : BadRequest(new { Success = false, Errors = result.Errors.Select(e => e.Description) });
    }
}