
using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Enums.Users;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Products.UpdatePrice;

public sealed class UpdatePriceCommandHandler : IRequestHandler<UpdatePriceCommand, Result<int>>
{
    private readonly IDataContext _context;

    public UpdatePriceCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(UpdatePriceCommand command, CancellationToken ct)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId);

        if (product == null)
        {
            return Result<int>.Failure(Errors.ProductByIdNotFound(product.Id));
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId);

        if (user.Role == UserRoles.User)
        {
            return Result<int>.Failure(Errors.InvalidCredentials());
        }

        if (product.Price == command.Price)
        {
            return Result<int>.Failure(Errors.ProductPriceInvalid());
        }

        product.Price = command.Price;
        await _context.SaveChangesAsync(ct);

        return Result<int>.Success(product.Id);
    }
}
