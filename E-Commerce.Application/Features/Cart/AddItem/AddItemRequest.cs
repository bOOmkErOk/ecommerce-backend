namespace E_Commerce.Application.Features.Cart.AddItem;

public record AddItemRequest(
    int ProductId,
    int Quantity
);
