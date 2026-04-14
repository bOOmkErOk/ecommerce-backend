using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Wishlist;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.WishList;

public sealed class ToggleWishlistCommandHandler : IRequestHandler<ToggleWishlistCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public ToggleWishlistCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(ToggleWishlistCommand command, CancellationToken ct)
    {
        var existingItem = await _context.WishlistItems
            .FirstOrDefaultAsync(x => x.UserId == command.UserId && x.ProductId == command.ProductId, ct);

        if (existingItem != null)
        {
            _context.WishlistItems.Remove(existingItem);
            await _context.SaveChangesAsync(ct);
            return Result<bool>.Success(false);
        }


        var wishlistItem = new WishlistItem
        {
            UserId = command.UserId,
            ProductId = command.ProductId,
            CreatedAt = DateTime.UtcNow
        };

        _context.WishlistItems.Add(wishlistItem);
        await _context.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
