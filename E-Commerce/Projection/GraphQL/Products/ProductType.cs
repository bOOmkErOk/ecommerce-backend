using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Projection.GraphQL.Products;

public sealed class ProductType : ObjectType<Product>
{
    protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor.Field(p => p.Id);
        descriptor.Field(p => p.Title);
        descriptor.Field(p => p.Price);
        descriptor.Field(p => p.Description);
        descriptor.Field(p => p.ImageUrl);
        descriptor.Field(p => p.Category);
        descriptor.Field(p => p.Stock);
        descriptor.Field(p => p.Ratings);
        descriptor.Field(p => p.IsOnSale);
        descriptor.Field(p => p.TotalSold);
        descriptor.Field(p => p.DiscountPercent);
        descriptor.Field(p => p.SponsoredUntil);
        descriptor.Field(p => p.IsSponsored);
        descriptor.Field("averageRating")
            .Resolve(ctx =>
            {
                var product = ctx.Parent<Product>();
                if (product.Ratings == null || !product.Ratings.Any())
                    return 0.0;
                return product.Ratings.Average(r => r.Value);
            });
    }
}
