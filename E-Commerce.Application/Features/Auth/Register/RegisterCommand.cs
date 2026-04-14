
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Auth.Register;

public record RegisterCommand
(
    string Name,
    string Email,
    string Password,
    string PhoneNumber
    ) : IRequest<Result>;
