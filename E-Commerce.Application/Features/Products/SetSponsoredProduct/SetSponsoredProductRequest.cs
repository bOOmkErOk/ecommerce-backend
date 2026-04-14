namespace E_Commerce.Application.Features.Products.SetSponsoredProduct;

public record SetSponsoredProductRequest(
    int ProductId,
    DateTime SponsoredUntil
);
