
using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Auth.ResetPassword;

public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public ResetPasswordCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(ResetPasswordCommand command, CancellationToken ct)
    {
        var user = await _context.Users
      .FirstOrDefaultAsync(u => u.Email == command.Email, ct);
        if (user == null)
            return Result<bool>.Failure(Errors.UserNotFound());

        var verification = await _context.EmailVerifications
            .FirstOrDefaultAsync(v => v.UserId == user.Id, ct);
        if (verification != null)
            _context.EmailVerifications.Remove(verification);

        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
