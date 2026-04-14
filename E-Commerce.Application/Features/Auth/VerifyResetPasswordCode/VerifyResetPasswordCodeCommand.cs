

using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Auth.VerifyResetPasswordCode;

public sealed record VerifyResetPasswordCodeCommand
(
    string Email,
    string Code
) : IRequest<Result<bool>>;
