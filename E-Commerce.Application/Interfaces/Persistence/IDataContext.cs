using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Entities.Carts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.Entities.Wishlist;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Interfaces.Persistence;

public interface IDataContext
{
    //User
    DbSet<User> Users { get; }
    DbSet<Address> Addresses { get; }


    //Products
    DbSet<Product> Products { get; }

    //Cart
    DbSet<Cart> Carts { get; }
    DbSet<CartItem> CartItems { get; }

    //Orders
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }

    //Rating
    DbSet<Rating> Ratings { get; }

    //EmailVerification
    DbSet<EmailVerification> EmailVerifications { get; }

    //WishList
    DbSet<WishlistItem> WishlistItems { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}
