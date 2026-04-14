using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Projection.GraphQL.Users;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(u => u.Id);
        descriptor.Field(u => u.Name);
        descriptor.Field(u => u.Email);
        descriptor.Field(u => u.Role);
        descriptor.Field(u => u.PhoneNumber);


        descriptor.Ignore(u => u.HashedPassword);
        descriptor.Ignore(u => u.Cart);
    }
}
