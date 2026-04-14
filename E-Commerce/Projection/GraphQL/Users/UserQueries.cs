using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Projection.GraphQL.Users;

[QueryType]
public class UserQueries
{
    public async Task<User?> GetMe(
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            throw new GraphQLException("User not authenticated");

        return await context.Users
            .Include(u => u.Addresses)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}