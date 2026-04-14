using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Enums.Orders;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace E_Commerce.Projection.GraphQL.Orders;


[QueryType]
public sealed class OrderQueries
{

    public async Task<List<Order>> GetMyOrders(
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            throw new GraphQLException("User not authenticated");

        return await context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Product)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderById(
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http,
        int Id)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            throw new GraphQLException("User not authenticated");

        var order = await context.Orders
    .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
    .FirstOrDefaultAsync(o => o.Id == Id && o.UserId == userId);

        if (order == null) throw new GraphQLException("Order not found");

        return order;

    }

    [Authorize]
    [GraphQLName("cancelledOrders")]
    public async Task<List<Order>> GetCancelledOrders(
     [Service] IDataContext context,
     [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            throw new GraphQLException("User not authenticated");

        return await context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId && o.Status == OrderStatus.Cancelled)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}
