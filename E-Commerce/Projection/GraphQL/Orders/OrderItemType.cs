using E_Commerce.Domain.Entities.Orders;

namespace E_Commerce.Projection.GraphQL.Orders;

public class OrderItemType : ObjectType<OrderItem>
{
    protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
    {
        descriptor.Field(o => o.Id);
        descriptor.Field(o => o.Price);
        descriptor.Field(o => o.Quantity);
        descriptor.Field(o => o.Product);

        descriptor.Ignore(o => o.Order);
        descriptor.Ignore(o => o.OrderId);
        descriptor.Ignore(o => o.ProductId);
    }
}
