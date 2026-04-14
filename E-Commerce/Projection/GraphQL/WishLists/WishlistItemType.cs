using E_Commerce.Domain.Entities.Wishlist;

namespace E_Commerce.Projection.GraphQL.WishLists;

public class WishlistItemType : ObjectType<WishlistItem>
{
    protected override void Configure(IObjectTypeDescriptor<WishlistItem> descriptor)
    {
        descriptor.Field(w => w.Id);
        descriptor.Field(w => w.Product);
        descriptor.Field(w => w.CreatedAt);
        descriptor.Ignore(x => x.User);
        descriptor.Ignore(w => w.ProductId);
        descriptor.Ignore(w => w.UserId);
    }
}
