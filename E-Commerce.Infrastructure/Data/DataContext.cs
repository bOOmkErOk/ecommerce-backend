using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Entities.Carts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.Entities.Wishlist;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Infrastructure.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<EmailVerification> EmailVerifications { get; set; }

    public DbSet<WishlistItem> WishlistItems { get; set; }
}
