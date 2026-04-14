
using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Auth.VerifyResetPasswordCode;

public sealed class VerifyResetPasswordCodeCommandHandler : IRequestHandler<VerifyResetPasswordCodeCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public VerifyResetPasswordCodeCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(VerifyResetPasswordCodeCommand command, CancellationToken ct)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == command.Email, ct);
        if (user == null)
            return Result<bool>.Failure(Errors.UserNotFound());

        var verification = await _context.EmailVerifications
            .FirstOrDefaultAsync(v => v.UserId == user.Id, ct);
        if (verification == null)
            return Result<bool>.Failure(Errors.NotFound());

        if (verification.Code != command.Code)
            return Result<bool>.Failure(Errors.InvalidCode());

        if (verification.ExpiresAt < DateTime.UtcNow)
            return Result<bool>.Failure(Errors.CodeExpired());

        return Result<bool>.Success(true);
    }

}
