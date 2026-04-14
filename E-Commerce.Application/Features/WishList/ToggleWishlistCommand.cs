using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.WishList;

public record ToggleWishlistCommand(int UserId, int ProductId) : IRequest<Result<bool>>;
