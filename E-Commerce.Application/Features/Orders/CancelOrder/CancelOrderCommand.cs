
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Orders.CancelOrder;

public record CancelOrderCommand
(
    int UserId,
    int OrderId
    ) : IRequest<Result<bool>>;
