using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Enums.Users;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Products.AddProduct;

public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<int>>
{
    private readonly IDataContext _context;

    public AddProductCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(AddProductCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId);

        if (user.Role == UserRoles.User)
        {
            return Result<int>.Failure(Errors.InvalidCredentials());
        }

        Product product = new Product
        {
            Title = command.Title,
            Price = command.Price,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            Category = command.Category,
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(ct);

        return Result<int>.Success(product.Id);
    }
}
