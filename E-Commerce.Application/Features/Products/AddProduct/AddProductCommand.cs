
using E_Commerce.Domain.Enums.Products;
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Products.AddProduct;

public record AddProductCommand
(
    string Title,
    decimal Price,
    string Description,
    string ImageUrl,
    Category Category,
    int UserId
    ) : IRequest<Result<int>>;
