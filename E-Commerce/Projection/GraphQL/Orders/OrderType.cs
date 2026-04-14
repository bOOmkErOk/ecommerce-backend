using E_Commerce.Domain.Entities.Orders;

namespace E_Commerce.Projection.GraphQL.Orders;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor.Field(o => o.Id);
        descriptor.Field(o => o.CreatedAt);
        descriptor.Field(o => o.OrderItems);
        descriptor.Field(o => o.Status);

        descriptor.Ignore(o => o.User);
        descriptor.Ignore(o => o.UserId);
    }
}
