using Resend;
using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Site")]

public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;
    private readonly IResend _resend; // Interface correta
    private readonly IConfiguration _configuration;

    public AuthController(
        IIdentityService identityService, 
        UserManager<User> userManager,
        IResend resend, // Injete a interface IResend
        IConfiguration configuration)
    {
        _userManager = userManager;
        _identityService = identityService;
        _resend = resend;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            
            var userId = await _identityService.RegisterUserAsync(request);
    
            var user = await _userManager.FindByIdAsync(userId.ToString());
            
            if (user == null)
                return BadRequest("Usuário não encontrado após o registro.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(
                action: "Confirm", // Nome do método
                controller: "ConfirmEmail", // Nome do controller SEM "Controller"
                values: new { userId = user.Id, code = token },
                protocol: Request.Scheme);
            
            await SendConfirmationEmail(user.Email!, confirmationLink!);

            return Ok(new { Message = "Usuário registrado com sucesso", UserId = userId });
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
        Subject = "Por favor, confirme seu endereço de e-mail",
        HtmlBody = $"""
            <html>
                <body>
                    <h2>Confirmação de Cadastro</h2>
                    <p>Confirme seu endereço de e-mail para aumentar a segurança da sua conta.</p>
                    <p>Clique <a href="{confirmationLink}">aqui</a> para confirmar.</p>

                </body>
            </html>
            """
    };
        await _resend.EmailSendAsync(request);
    }
}