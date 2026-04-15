using E_Commerce.Application.Interfaces.SMTP;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace E_Commerce.Infrastructure.SMTP;

public sealed class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string subject, string body, string email)
    {
        var senderEmail = _config["EmailSettings:Email"];
        var appPassword = _config["EmailSettings:Password"];

        using var mail = new MailMessage();
        mail.From = new MailAddress(senderEmail!, "E-Commerce");
        mail.IsBodyHtml = true;
        mail.Subject = subject;
        mail.To.Add(email);
        mail.Body = body;

        using var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, appPassword),
        };

        await smtpClient.SendMailAsync(mail);
    }
}
