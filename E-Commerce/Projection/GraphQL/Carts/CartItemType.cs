using E_Commerce.Domain.Entities.Carts;

namespace E_Commerce.Projection.GraphQL.Carts;

public sealed class CartItemType : ObjectType<CartItem>
{
    protected override void Configure(IObjectTypeDescriptor<CartItem> descriptor)
    {
        descriptor.Field(ci => ci.Id);
        descriptor.Field(ci => ci.Quantity);
        descriptor.Field(ci => ci.Product);
        descriptor.Ignore(ci => ci.CartId);
        descriptor.Ignore(ci => ci.Cart);
        descriptor.Ignore(ci => ci.ProductId);
    }
}
