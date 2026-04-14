using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Users.UpdateUser;

public record UpdateUserCommand
(
    string Name,
    string Email,
    string PhoneNumber,
    string? CurrentPassword,
    int UserId

    ) : IRequest<Result<bool>>;
