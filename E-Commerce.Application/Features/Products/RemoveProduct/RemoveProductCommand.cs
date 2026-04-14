
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Products.RemoveProduct;

public record RemoveProductCommand
(
     int UserId,
     int ProductId
    ) : IRequest<Result<int>>;
