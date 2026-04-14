using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Enums.Products;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Projection.GraphQL.Products;

[QueryType]
public sealed class ProductQueries
{
    public async Task<Product?> GetProductById(
        int id,
        [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetProducts(
     [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .OrderBy(p => Guid.NewGuid())
            .Take(8)
            .ToListAsync();
    }

    public async Task<List<Product>> GetSaleProducts(
        [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .Where(p => p.DiscountPercent > 0)
            .OrderByDescending(p => p.DiscountPercent)
            .Take(4)
            .ToListAsync();
    }

    public async Task<List<Product>> GetBestSellingProducts(
        [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .OrderByDescending(p => p.TotalSold)
            .Take(4)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllProducts(
    [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllSaleProducts(
        [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .Where(p => p.DiscountPercent > 0)
            .OrderByDescending(p => p.DiscountPercent)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllBestSellingProducts(
        [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .OrderByDescending(p => p.TotalSold)
            .ToListAsync();
    }

    public async Task<List<Product>> GetNewArrivalProducts(
    [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .OrderByDescending(p => p.CreatedAt)
            .Take(4)
            .ToListAsync();
    }

    public async Task<Product?> GetSponsoredProduct(
    [Service] IDataContext context)
    {
        return await context.Products
            .FirstOrDefaultAsync(p => p.IsSponsored);
    }

    public async Task<List<Product>> SearchProducts(
    string query,
    [Service] IDataContext context)
    {
        return await context.Products
            .Include(p => p.Ratings)
            .Where(p => p.Title.Contains(query) || p.Description.Contains(query))
            .ToListAsync();
    }

    public async Task<bool> HasUserBoughtProduct(
    int productId,
    [Service] IDataContext context,
    [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return false;

        return await context.Orders
            .AnyAsync(o => o.UserId == userId &&
                o.OrderItems.Any(oi => oi.ProductId == productId));
    }

    [UsePaging]
    [UseFiltering]
    public IQueryable<Product> GetProductsByCategory(
    [Service] IDataContext context,
    Category category)
    {
        return context.Products.Where(p => p.Category == category);
    }

}
