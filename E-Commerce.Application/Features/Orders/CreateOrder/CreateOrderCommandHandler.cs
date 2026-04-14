using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Enums.Orders;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Orders.CreateOrder;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<int>>
{
    private readonly IDataContext _context;

    public CreateOrderCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateOrderCommand command, CancellationToken ct)
    {
        var cart = await _context.Carts
        .Include(c => c.Items)
        .ThenInclude(ci => ci.Product)
        .FirstOrDefaultAsync(c => c.UserId == command.UserId, ct);

        if (cart == null || !cart.Items.Any())
            return Result<int>.Failure(Errors.CartEmpty());

        foreach (var item in cart.Items)
        {
            if (item.Quantity > item.Product.Stock)
                return Result<int>.Failure(Errors.ProductOutOfStock());
        }

        Order order = new Order
        {
            UserId = command.UserId,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            OrderItems = cart.Items.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                Price = ci.Product.Price
            }).ToList(),
        };

        _context.Orders.Add(order);

        _context.CartItems.RemoveRange(cart.Items);

        foreach (var item in cart.Items)
        {
            item.Product.Stock -= item.Quantity;
            item.Product.TotalSold += item.Quantity;
        }

        await _context.SaveChangesAsync(ct);

        return Result<int>.Success(order.Id);
    }
}
