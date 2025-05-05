using Resend;
using Microsoft.Extensions.Options;

public class EmailConfig
{
    public required string FromEmail { get; set; }
    public required string FromName { get; set; }
}

public class ResendEmailService
{
    private readonly IResend _resend;
    private readonly EmailConfig _config;

    public ResendEmailService(IResend resend, IOptions<EmailConfig> config)
    {
        _resend = resend;
        _config = config.Value;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string htmlContent,
        string? textContent = null)
    {
        var message = new EmailMessage
        {
            From = _config.FromEmail,
            To = { to },
            Subject = subject,
            HtmlBody = htmlContent,
            TextBody = textContent ?? StripHtml(htmlContent)
        };

        await _resend.EmailSendAsync(message);
    }

    private static string StripHtml(string html)
    {
        // Implementação simples para criar texto plano a partir de HTML
        return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", "");
    }
}