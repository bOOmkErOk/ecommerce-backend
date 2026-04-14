using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Cart.AddItem;

public record AddItemCommand
(
      int ProductId,
      int Quantity,
      int UserId

    ) : IRequest<Result<int>>;
