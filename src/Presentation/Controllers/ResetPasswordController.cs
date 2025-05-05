using System.Net;
using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ResetPasswordController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    public ResetPasswordController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassowordDTO request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound(new ProblemDetails
                {
                    Title = "Usuário não encontrado",
                    Detail = $"Nenhum usuário encontrado com o ID {request.UserId}",
                    Status = 404
                });
            
            var decodedCode = WebUtility.UrlDecode(request.resetCode);

            var result = await _userManager.ResetPasswordAsync(user, decodedCode, request.newPassword);
            if (!result.Succeeded)
                return BadRequest(new ProblemDetails
                {
                    Title = "Erro ao redefinir a senha",
                    Detail = string.Join(", ", result.Errors.Select(e => e.Description)),
                    Status = 400
                });

            return Ok(new { Message = "Senha redefinida com sucesso" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
