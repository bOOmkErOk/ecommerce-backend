using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Application.Interfaces.SMTP;
using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<bool>>
{
    private readonly IDataContext _context;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(IDataContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<Result<bool>> Handle(ForgotPasswordCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == command.Email, ct);
        if (user == null)
            return Result<bool>.Failure(Errors.UserNotFound());

        // remove existing verification if any
        var existing = await _context.EmailVerifications
            .FirstOrDefaultAsync(v => v.UserId == user.Id, ct);
        if (existing != null)
            _context.EmailVerifications.Remove(existing);

        var verification = EmailVerification.Create();
        verification.SetUserId(user.Id);
        _context.EmailVerifications.Add(verification);
        await _context.SaveChangesAsync(ct);

        try
        {
            await _emailService.SendEmailAsync(
                "Reset your password",
                $"<h1>Hi {user.Name}!</h1><p>Your password reset code is: <strong>{verification.Code}</strong></p><p>Code expires in 15 minutes.</p>",
                user.Email
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("EMAIL SENDING FAILED!");
            Console.WriteLine(ex.ToString());
            Console.WriteLine("*********************************");

            return Result<bool>.Failure(new Errors(500, "Email delivery failed: " + ex.Message));
        }

        return Result<bool>.Success(true);
    }
}
