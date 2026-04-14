using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Cart.UpdateQuantity;

public record UpdateQuantityCommand
(

    int ItemId,
    int Quantity,
    int UserId
    ) : IRequest<Result<bool>>;
