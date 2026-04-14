
using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.Enums.Orders;

namespace E_Commerce.Domain.Entities.Orders;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<OrderItem> OrderItems { get; set; }
    public User User { get; set; }

    public OrderStatus Status { get; set; }
}
