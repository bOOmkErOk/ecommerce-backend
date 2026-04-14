using E_Commerce.Domain.Results;
using MediatR;


namespace E_Commerce.Application.Features.Orders.CreateOrder;

public record CreateOrderCommand
(
        int UserId
    ) : IRequest<Result<int>>;
