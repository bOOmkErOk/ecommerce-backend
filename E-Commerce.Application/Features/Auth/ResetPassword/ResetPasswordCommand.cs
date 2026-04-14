
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Auth.ResetPassword;

public sealed record ResetPasswordCommand(
    string Email,
    string NewPassword
) : IRequest<Result<bool>>;
