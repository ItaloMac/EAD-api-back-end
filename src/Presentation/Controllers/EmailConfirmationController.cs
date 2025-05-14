using System;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resend;

namespace InvictusAPI.Presentation.Controllers{
    [ApiController]
    [Route("api/[controller]")]

    public class EmailConfirmationController : ControllerBase{
        private readonly UserManager<User> _userManager;
        private readonly IResend _resend;

        public EmailConfirmationController(
            UserManager<User> userManager,
            IResend resend) 
        {
            _userManager = userManager;
            _resend = resend;
        }

        [HttpPost("check-email-confirmation")]
        public async Task<IActionResult> CheckEmailConfirmation([FromBody] CheckEmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { error = "Email não encontrado" });
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(
                    action: "Confirm", // Nome do método
                    controller: "ConfirmEmail", // Nome do controller SEM "Controller"
                    values: new { userId = user.Id, code = token },
                    protocol: Request.Scheme);
                
                await SendConfirmationEmail(user.Email!, confirmationLink!);
                    return BadRequest(new { error = "Acesse seu email e confirme a sua conta através do link enviado"});
            }

            return Ok(new { message = "Email confirmado" });
        }

        public class CheckEmailRequest
        {
            public required string Email { get; set; }
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
} 

    