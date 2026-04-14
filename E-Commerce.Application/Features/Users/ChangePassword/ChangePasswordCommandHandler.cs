using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Users.ChangePassword;

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public ChangePasswordCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(ChangePasswordCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, ct);
        if (user == null)
            return Result<bool>.Failure(Errors.UserNotFound());

        var isValidPassword = BCrypt.Net.BCrypt.Verify(command.CurrentPassword, user.HashedPassword);
        if (!isValidPassword)
            return Result<bool>.Failure(Errors.InvalidCredentials());

        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
