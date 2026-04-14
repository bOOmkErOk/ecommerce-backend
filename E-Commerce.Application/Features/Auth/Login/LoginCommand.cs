using MediatR;
using E_Commerce.Domain.Results;

namespace E_Commerce.Application.Features.Auth.Login;

public record LoginCommand
(
    string Email,
    string Password
) : IRequest<Result<LoginResponse>>;
