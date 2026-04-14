using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Products.SetSponsoredProduct;

public record SetSponsoredProductCommand(
    int ProductId,
    DateTime SponsoredUntil
) : IRequest<Result<bool>>;
