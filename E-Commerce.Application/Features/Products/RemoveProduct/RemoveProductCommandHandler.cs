using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Enums.Users;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.Application.Features.Products.RemoveProduct;

public sealed class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Result<int>>
{
    private readonly IDataContext _context;

    public RemoveProductCommandHandler(IDataContext context) => _context = context;


    public async Task<Result<int>> Handle(RemoveProductCommand command, CancellationToken ct)
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

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(ct);
        return Result<int>.Success(product.Id);
    }
}
