using E_Commerce.Domain.Entities.Users;
namespace E_Commerce.Domain.Entities.Carts;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}
