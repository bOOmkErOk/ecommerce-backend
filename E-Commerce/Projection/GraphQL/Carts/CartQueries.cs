using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Carts;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Projection.GraphQL.Carts;

[QueryType]
public sealed class CartQueries
{
    [Authorize]
    public async Task<Cart?> GetMyCart(
         [Service] IDataContext context,
         ClaimsPrincipal claimsUser)
    {
        var userIdClaim = claimsUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            throw new GraphQLException("User not authenticated");

        return await context.Carts
            .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}
