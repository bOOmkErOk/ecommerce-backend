

using E_Commerce.Domain.Enums.Orders;
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Orders.UpdateStatus;

public record UpdateStatusCommand
(
      int OrderId,
      OrderStatus NewStatus
    ) : IRequest<Result<bool>>;
