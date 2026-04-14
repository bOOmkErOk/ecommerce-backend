using E_Commerce.Domain.Enums.Products;
using E_Commerce.Projection.GraphQL.Carts;
using E_Commerce.Projection.GraphQL.Orders;
using E_Commerce.Projection.GraphQL.Products;
using E_Commerce.Projection.GraphQL.Users;
using E_Commerce.Projection.GraphQL.WishLists;

namespace E_Commerce.Extensions.GraphQL;

public static class GraphqlExtensions
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType()
            .AddAuthorization()
            .AddFiltering()
            .AddProjections()
            .AddSorting()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .AddType(new EnumType<Category>(d => d.Name("Category")))
            .BindRuntimeType<Category, StringType>()
            .AddTypeExtension<ProductQueries>()
            .AddType<ProductType>()
            .AddTypeExtension<CartQueries>()
            .AddType<CartType>()
            .AddType<CartItemType>()
            .AddTypeExtension<OrderQueries>()
            .AddType<OrderType>()
            .AddType<OrderItemType>()
            .AddTypeExtension<UserQueries>()
            .AddType<UserType>()
            .AddType<AddressType>()
            .AddTypeExtension<WishlistItemQueries>()
            .AddType<WishlistItemType>();




        return services;
    }
}
