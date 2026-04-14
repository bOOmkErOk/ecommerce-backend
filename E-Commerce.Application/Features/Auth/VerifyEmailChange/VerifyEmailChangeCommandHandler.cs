using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Application.Features.Auth.VerifyEmailChange;

public sealed class VerifyEmailChangeCommandHandler : IRequestHandler<VerifyEmailChangeCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public VerifyEmailChangeCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(VerifyEmailChangeCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(u => u.EmailVerification)
            .FirstOrDefaultAsync(u => u.Id == command.UserId, ct);

        if (user == null)
            return Result<bool>.Failure(Errors.UserNotFound());

        if (user.PendingEmail == null)
            return Result<bool>.Failure(new Errors(400, "No pending email change"));

        if (user.EmailVerification == null || user.EmailVerification.Code != command.Code)
            return Result<bool>.Failure(new Errors(400, "Invalid verification code"));

        user.Email = user.PendingEmail;
        user.PendingEmail = null;
        _context.EmailVerifications.Remove(user.EmailVerification);

        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}