

using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Auth.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : IRequest<Result<bool>>;