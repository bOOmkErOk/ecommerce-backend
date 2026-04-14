

using E_Commerce.Domain.Enums.Orders;

namespace E_Commerce.Application.Features.Orders.UpdateStatus;

public record UpdateStatusRequest
(
    int OrderId,
    OrderStatus NewStatus
    );
