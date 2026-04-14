using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Enums.Orders;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Orders.CancelOrder;

public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public CancelOrderCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(CancelOrderCommand command, CancellationToken ct)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == command.OrderId && o.UserId == command.UserId);

        if (order == null)
        {
            return Result<bool>.Failure(Errors.OrderNotFound());
        }
        if (order.Status == OrderStatus.Cancelled)
            return Result<bool>.Failure(Errors.OrderAlreadyCancelled());

        if (order.Status == OrderStatus.Delivered)
            return Result<bool>.Failure(Errors.OrderCannotBeCancelled());

        order.Status = OrderStatus.Cancelled;
        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}