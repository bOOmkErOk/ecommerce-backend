

using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Application.Interfaces.SMTP;
using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Users.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<bool>>
{
    private readonly IDataContext _context;
    private readonly IEmailService _emailService;

    public UpdateUserCommandHandler(IDataContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<Result<bool>> Handle(UpdateUserCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, ct);

        if (user == null)
            return Result<bool>.Failure(Errors.UserNotFound());

        if (command.Name.Length < 3 || command.Name.Length > 15)
            return Result<bool>.Failure(Errors.UsernameInvalid());

        if (!string.IsNullOrEmpty(command.CurrentPassword))
        {
            var isValidPassword = BCrypt.Net.BCrypt.Verify(command.CurrentPassword, user.HashedPassword);
            if (!isValidPassword) return Result<bool>.Failure(Errors.InvalidCredentials());
        }

        if (string.IsNullOrWhiteSpace(command.PhoneNumber))
        {
            return Result<bool>.Failure(Errors.PhoneEmpty());
        }

        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            var phoneRegex = @"^\+?[0-9]{9,15}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(command.PhoneNumber, phoneRegex))
            {
                return Result<bool>.Failure(Errors.PhoneInvalid());
            }

            var phoneExists = await _context.Users
                .AnyAsync(u => u.PhoneNumber == command.PhoneNumber && u.Id != command.UserId, ct);

            if (phoneExists)
                return Result<bool>.Failure(Errors.PhoneExists());
        }

        if (user.Email != command.Email)
        {
            if (string.IsNullOrEmpty(command.CurrentPassword))
                return Result<bool>.Failure(Errors.PasswordRequiredForEmailChange());

            if (!System.Text.RegularExpressions.Regex.IsMatch(command.Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return Result<bool>.Failure(Errors.EmailInvalid());

            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == command.Email && u.Id != command.UserId, ct);

            if (emailExists)
                return Result<bool>.Failure(Errors.EmailExists());

            user.PendingEmail = command.Email;

            var existing = await _context.EmailVerifications
                .FirstOrDefaultAsync(v => v.UserId == user.Id, ct);
            if (existing != null)
                _context.EmailVerifications.Remove(existing);

            var verification = EmailVerification.Create();
            verification.SetUserId(user.Id);
            _context.EmailVerifications.Add(verification);

            try
            {
                await _emailService.SendEmailAsync(
                    "Verify your new email",
                    $"<h1>Hi {user.Name}!</h1><p>Your email verification code is: <strong>{verification.Code}</strong></p>",
                    command.Email
                );
            }
            catch (Exception ex)
            {
                _context.EmailVerifications.Remove(verification);

                user.PendingEmail = null;

                await _context.SaveChangesAsync(ct);

                return Result<bool>.Failure(Errors.EmailSendFailed(ex.Message));

            }
        }

        user.Name = command.Name;
        user.PhoneNumber = command.PhoneNumber;

        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
