using E_Commerce.Domain.Entities.Carts;

namespace E_Commerce.Projection.GraphQL.Carts;

public sealed class CartType : ObjectType<Cart>
{
    protected override void Configure(IObjectTypeDescriptor<Cart> descriptor)
    {
        descriptor.Field(c => c.Id);
        descriptor.Field(c => c.Items);

        descriptor.Ignore(c => c.UserId);
        descriptor.Ignore(c => c.User);

    }
}
