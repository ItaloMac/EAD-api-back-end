using Application.DTOs;
using Application.DTOs.Site;
using Application.Interfaces;
using InvictusAPI.jwt;
using Microsoft.AspNetCore.Mvc;
using Resend;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Site")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IApplicationDbContext _context;
    private readonly IResend _resend;

    public AuthController(
        IAuthService authService,
        IApplicationDbContext context,
        IResend resend)
    {
        _authService = authService;
        _context = context;
        _resend = resend;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            var userId = await _authService.RegisterUserAsync(request);

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return BadRequest("Erro ao registrar o usuário.");

            // (Opcional) Criar link de confirmação fictício
            var confirmationLink = $"https://seusite.com/confirmar-email/{user.Id}";

            await SendConfirmationEmail(user.Email!, confirmationLink);

            return Ok(new
            {
                Message = "Usuário registrado com sucesso.",
                UserId = userId
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    private async Task SendConfirmationEmail(string email, string confirmationLink)
    {
        var request = new EmailMessage
        {
            From = "onboarding@resend.dev",
            To = { email },
            Subject = "Confirme seu endereço de e-mail",
            HtmlBody = $"""
                <html>
                    <body>
                        <h2>Confirmação de Cadastro</h2>
                        <p>Olá! Confirme seu e-mail para ativar sua conta.</p>
                        <p><a href="{confirmationLink}">Clique aqui para confirmar</a></p>
                    </body>
                </html>
            """
        };

        await _resend.EmailSendAsync(request);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        try
        {
            var token = await _authService.LoginUserAsync(dto);
            return Ok(new { Token = token });
        }
         catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        }
}
