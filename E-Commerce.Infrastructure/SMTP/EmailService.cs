using E_Commerce.Application.Interfaces.SMTP;
using System.Net;
using System.Net.Mail;

namespace E_Commerce.Infrastructure.SMTP;

public sealed class EmailService : IEmailService
{
    private readonly string _email = "codesell.web@gmail.com";
    private readonly string _password = "psvn zhpr yyau oliu";

    public async Task SendEmailAsync(string subject, string body, string email)
    {
        using var mail = new MailMessage();
        mail.From = new MailAddress(_email, "E-Commerce");
        mail.IsBodyHtml = true;
        mail.Subject = subject;
        mail.To.Add(email);
        mail.Body = body;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(_email, _password),
        };

        await smtpClient.SendMailAsync(mail);
    }
}
