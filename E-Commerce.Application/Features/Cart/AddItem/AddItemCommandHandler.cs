using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Carts;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Cart.AddItem;

public sealed class AddItemCommandHandler : IRequestHandler<AddItemCommand, Result<int>>
{
    private readonly IDataContext _context;

    public AddItemCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(AddItemCommand command, CancellationToken ct)
    {
        var product = await _context.Products.FindAsync(command.ProductId);
        if (product == null)
            return Result<int>.Failure(Errors.ProductNotFound());

        var cart = await _context.Carts
            .FirstOrDefaultAsync(c => c.UserId == command.UserId, ct);
        if (cart == null)
            return Result<int>.Failure(Errors.CartNotFound());

        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(ci =>
                ci.CartId == cart.Id &&
                ci.ProductId == command.ProductId, ct);

        int currentInCart = existingItem?.Quantity ?? 0;
        int totalAfterAdd = currentInCart + command.Quantity;

        if (totalAfterAdd > product.Stock)
        {
            return Result<int>.Failure(Errors.ProductOutOfStock());
        }

        if (existingItem != null)
        {
            existingItem.Quantity += command.Quantity;
        }
        else
        {
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = command.ProductId,
                Quantity = command.Quantity
            };
            _context.CartItems.Add(cartItem);
        }

        await _context.SaveChangesAsync(ct);
        return Result<int>.Success(cart.Id);
    }
}