using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Auth.VerifyEmailChange;


public sealed record VerifyEmailChangeCommand(int UserId, string Code) : IRequest<Result<bool>>;
