

using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Cart.UpdateQuantity;

public sealed class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public UpdateQuantityCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateQuantityCommand command, CancellationToken ct)
    {

        if (command.Quantity <= 0)
        {
            return Result<bool>.Failure(Errors.InvalidQuantity());
        }
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == command.ItemId && ci.Cart.UserId == command.UserId);

        if (cartItem == null)
        {
            return Result<bool>.Failure(Errors.CartNotFound());
        }

        var product = await _context.Products.FindAsync(cartItem.ProductId);
        if (command.Quantity > product.Stock)
            return Result<bool>.Failure(Errors.ProductOutOfStock());

        cartItem.Quantity = command.Quantity;
        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }

}
