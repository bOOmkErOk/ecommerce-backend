using E_Commerce.Domain.Entities.Users;
namespace E_Commerce.Projection.GraphQL.Users;

public class AddressType : ObjectType<Address>
{
    protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
    {
        descriptor.Field(a => a.Id);
        descriptor.Field(a => a.StreetAddress);
        descriptor.Field(a => a.Apartment);
        descriptor.Field(a => a.City);

        descriptor.Ignore(a => a.UserId);
        descriptor.Ignore(a => a.User);
    }
}
