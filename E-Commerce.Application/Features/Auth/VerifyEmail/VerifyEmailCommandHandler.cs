using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Auth.VerifyEmail;

public sealed class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result<int>>
{
    private readonly IDataContext _context;

    public VerifyEmailCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(VerifyEmailCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == command.Email, ct);
        if (user == null)
            return Result<int>.Failure(Errors.UserNotFound());

        var verification = await _context.EmailVerifications
            .FirstOrDefaultAsync(v => v.UserId == user.Id, ct);

        if (verification == null)
            return Result<int>.Failure(Errors.NotFound());

        if (verification.Code != command.Code)
            return Result<int>.Failure(Errors.InvalidCode());

        if (verification.ExpiresAt < DateTime.UtcNow)
            return Result<int>.Failure(Errors.CodeExpired());

        user.IsEmailVerified = true;
        _context.EmailVerifications.Remove(verification);

        await _context.SaveChangesAsync(ct);
        return Result<int>.Success(user.Id);
    }
}
