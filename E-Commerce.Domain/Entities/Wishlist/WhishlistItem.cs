
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Domain.Entities.Wishlist;

public class WishlistItem
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int ProductId { get; set; }

    public User User { get; set; } = null!;
    public Product Product { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
