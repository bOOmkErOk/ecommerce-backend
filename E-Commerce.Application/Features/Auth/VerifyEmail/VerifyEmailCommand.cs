

using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Auth.VerifyEmail;

public record VerifyEmailCommand
(
string Email,
string Code
    ) : IRequest<Result<int>>;
