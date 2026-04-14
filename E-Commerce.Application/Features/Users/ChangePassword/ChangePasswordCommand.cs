using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Users.ChangePassword;

public record ChangePasswordCommand(
    int UserId,
    string CurrentPassword,
    string NewPassword
) : IRequest<Result<bool>>;
