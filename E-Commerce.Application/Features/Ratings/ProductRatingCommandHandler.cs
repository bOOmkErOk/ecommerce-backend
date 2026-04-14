using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Ratings;

public sealed class ProductRatingCommandHandler : IRequestHandler<ProductRatingCommand, Result<int>>
{
    private readonly IDataContext _context;

    public ProductRatingCommandHandler(IDataContext context) => _context = context;

    public async Task<Result<int>> Handle(ProductRatingCommand command, CancellationToken ct)
    {

        var hasBought = await _context.Orders
    .AnyAsync(o => o.UserId == command.UserId &&
        o.OrderItems.Any(oi => oi.ProductId == command.ProductId), ct);

        if (!hasBought)
            return Result<int>.Failure(Errors.NotPurchased());

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId);

        if (product == null)
        {
            return Result<int>.Failure(Errors.ProductByIdNotFound(command.ProductId));
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId);

        if (user == null)
        {
            return Result<int>.Failure(Errors.UserByIdNotFound(command.UserId));
        }

        if (command.Rating > 5 || command.Rating <= 0)
        {
            return Result<int>.Failure(Errors.RatingInvalid());
        }
        var alreadyRated = await _context.Ratings.FirstOrDefaultAsync(r => r.UserId == command.UserId && r.ProductId == command.ProductId);

        if (alreadyRated != null)
        {
            return Result<int>.Failure(Errors.AlreadyRated());
        }

        Rating rating = new Rating
        {
            UserId = command.UserId,
            ProductId = command.ProductId,
            Value = command.Rating
        };


        _context.Ratings.Add(rating);

        await _context.SaveChangesAsync(ct);
        return Result<int>.Success(rating.Id);

    }

}
