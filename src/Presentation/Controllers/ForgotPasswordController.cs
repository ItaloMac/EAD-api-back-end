using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resend;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace InvictusAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IResend _resend;
        private readonly IConfiguration _configuration;

        public ForgotPasswordController(
            UserManager<User> userManager, 
            IResend resend,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _resend = resend;
            _configuration = configuration;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    // Por segurança, retorne sempre Ok mesmo quando o usuário não existe
                    return Ok(new { Message = "Se o email existir, um link de redefinição foi enviado" });
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = HttpUtility.UrlEncode(token);
                
                // Obter a URL do front-end da configuração
                var frontendUrl = _configuration["Frontend:BaseUrl"];
                var resetPath = _configuration["Frontend:ResetPasswordPath"] ?? "/reset-password";
                
                // Montar a URL do front-end com os parâmetros
                var resetLink = $"{frontendUrl}{resetPath}?userId={user.Id}&code={encodedToken}";

                await SendResetPasswordEmail(user.Email!, resetLink);

                return Ok(new { Message = "Email de redefinição de senha enviado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        private async Task SendResetPasswordEmail(string email, string resetLink)
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
                            <p>Clique <a href="{resetLink}">aqui</a> para redefinir sua senha.</p>
                            <p>Se você não solicitou esta redefinição, por favor ignore este email.</p>
                        </body>
                    </html>
                    """
            };
            
            await _resend.EmailSendAsync(request);
        }
    }
}