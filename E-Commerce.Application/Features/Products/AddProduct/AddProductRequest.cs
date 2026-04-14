
using E_Commerce.Domain.Enums.Products;

namespace E_Commerce.Application.Features.Products.AddProduct;

public record AddProductRequest
(
    string Title,
    decimal Price,
    string Description,
    string ImageUrl,
    Category Category
    );
