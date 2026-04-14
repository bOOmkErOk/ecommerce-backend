
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Cart.RemoveItem;

public record RemoveItemCommand
(
    int CartItemId,
    int UserId
    ) : IRequest<Result<bool>>;
