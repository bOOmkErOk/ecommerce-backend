
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Products.UpdatePrice;

public record UpdatePriceCommand
(
    int ProductId,
    decimal Price,
    int UserId
    ) : IRequest<Result<int>>;
