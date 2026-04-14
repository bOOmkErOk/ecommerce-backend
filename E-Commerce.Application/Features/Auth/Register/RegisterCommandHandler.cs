using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Application.Interfaces.SMTP;
using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CartEntity = E_Commerce.Domain.Entities.Carts.Cart;

namespace E_Commerce.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IDataContext _context;
    private readonly IEmailService _emailService;
    public RegisterCommandHandler(IDataContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<Result> Handle(RegisterCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result.Failure(Errors.UsernameEmpty());

        if (string.IsNullOrWhiteSpace(command.Email))
            return Result.Failure(Errors.EmailEmpty());

        if (!System.Text.RegularExpressions.Regex.IsMatch(command.Email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Result.Failure(Errors.EmailInvalid());

        if (string.IsNullOrWhiteSpace(command.Password))
            return Result.Failure(Errors.PasswordEmpty());

        if (command.Password.Length < 8)
            return Result.Failure(Errors.PasswordWeak());

        if (string.IsNullOrWhiteSpace(command.PhoneNumber) ||
            !System.Text.RegularExpressions.Regex.IsMatch(command.PhoneNumber, @"^\+?[0-9]{9,15}$"))
            return Result.Failure(Errors.PhoneInvalid());

        var phoneExists = await _context.Users.AnyAsync(u => u.PhoneNumber == command.PhoneNumber, ct);
        if (phoneExists)
        {
            return Result.Failure(Errors.PhoneExists());
        }

        var email = await _context.Users.FirstOrDefaultAsync(e => e.Email == command.Email);

        if (email != null)
        {
            return Result.Failure(Errors.EmailExists());
        }

        var hashedPass = BCrypt.Net.BCrypt.HashPassword(command.Password);

        User user = new User
        {
            Name = command.Name,
            Email = command.Email,
            HashedPassword = hashedPass,
            PhoneNumber = command.PhoneNumber,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(ct);

        var cart = new CartEntity
        {
            UserId = user.Id
        };

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync(ct);

        var verification = EmailVerification.Create();
        verification.SetUserId(user.Id);
        _context.EmailVerifications.Add(verification);
        await _context.SaveChangesAsync(ct);

        await _emailService.SendEmailAsync(
    "Verify your email",
    $"<h1>Hi {user.Name}!</h1><p>Your verification code is: <strong>{verification.Code}</strong></p><p>Code expires in 15 minutes.</p>",
    user.Email
);

        return Result.Success();
    }

}
