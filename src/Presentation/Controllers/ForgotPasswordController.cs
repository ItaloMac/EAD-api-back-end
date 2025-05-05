using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resend;

namespace InvictusAPI.Presentation.Controllers {

    [ApiController]
    [Route("api/[controller]")]

    public class ForgotPasswordController : ControllerBase{
        private readonly UserManager<User> _userManager;
        private readonly IResend _resend;

          // Construtor com injeção de dependência
            public ForgotPasswordController(UserManager<User> userManager, IResend resend)
            {
                _userManager = userManager;
                _resend = resend;
            }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return NotFound(new ProblemDetails{
                        Title = "Usuário não encontrado",
                        Detail = $"Nenhum usuário encontrado com o email {request.Email}",
                        Status = 404
                    });

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action(
                    action: "ResetPassword", // Nome do método
                    controller: "ResetPassword", // Nome do controller SEM "Controller"
                    values: new { userId = user.Id, code = token },
                    protocol: Request.Scheme);

                await SendResetPasswordEmail(user.Email!, resetLink!);

                return Ok(new { Message = "Email de redefinição de senha enviado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        private async Task SendResetPasswordEmail(string email, string confirmationLink)
        {
            var request = new EmailMessage
            {
                From = "onboarding@resend.dev",
                To = { email },
                Subject = "Redefinição de Senha",
                HtmlBody = $"""
                    <html>
                        <body>
                            <h2>Redefina a sua senha.</h2>
                            <p>Clique <a href="{confirmationLink}">aqui</a> para redefinir sua senha.</p>
                        </body>
                    </html>
                    """
            };
            
            await _resend.EmailSendAsync(request);
        }
    }
}