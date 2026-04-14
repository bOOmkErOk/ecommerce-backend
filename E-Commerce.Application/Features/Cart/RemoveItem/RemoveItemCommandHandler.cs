

using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Cart.RemoveItem;

public sealed class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public RemoveItemCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(RemoveItemCommand command, CancellationToken ct)
    {
        var itemInCart = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == command.CartItemId && ci.Cart.UserId == command.UserId);

        if (itemInCart == null)
        {
            return Result<bool>.Failure(Errors.CartItemNotFound());
        }


        _context.CartItems.Remove(itemInCart);
        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);

    }
}
