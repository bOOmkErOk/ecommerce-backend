using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Application.Interfaces.Security;
using E_Commerce.Application.Interfaces.SMTP;
using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Auth.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IDataContext _context;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _emailService;

    public LoginCommandHandler(IDataContext context, IJwtTokenService jwtTokenService, IEmailService emailService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(u => u.EmailVerification)
            .FirstOrDefaultAsync(u => u.Email == command.Email, ct);

        if (user == null)
            return Result<LoginResponse>.Failure(Errors.InvalidCredentials());

        var isValidPassword = BCrypt.Net.BCrypt.Verify(command.Password, user.HashedPassword);
        if (!isValidPassword)
            return Result<LoginResponse>.Failure(Errors.InvalidCredentials());

        if (!user.IsEmailVerified)
        {
            var newVerification = EmailVerification.Create();
            if (user.EmailVerification != null)
            {
                user.EmailVerification.Code = newVerification.Code;
                user.EmailVerification.ExpiresAt = DateTime.UtcNow.AddMinutes(15);
                user.EmailVerification.IsVerified = false;
            }
            else
            {
                user.EmailVerification = newVerification;
            }

            await _context.SaveChangesAsync(ct);

            await _emailService.SendEmailAsync(
    "Verify your email",
    $"Your code is: {user.EmailVerification.Code}",
    user.Email
);

            return Result<LoginResponse>.Failure(Errors.EmailNotVerified());
        }

        var token = _jwtTokenService.GenerateAccessToken(user);

        return Result<LoginResponse>.Success(new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            Name = user.Name
        });
    }
}
