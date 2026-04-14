using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Wishlist;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Projection.GraphQL.WishLists;

[QueryType]
public class WishlistItemQueries
{
    [Authorize]
    public IQueryable<WishlistItem> GetMyWishlist(
     [Service] IDataContext context,
     ClaimsPrincipal claimsUser)
    {
        var userIdString = claimsUser.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            throw new GraphQLException("User not authenticated");
        }

        return context.WishlistItems
            .Include(wi => wi.Product)
            .Where(wi => wi.UserId == userId)
            .OrderByDescending(wi => wi.CreatedAt);
    }
}
