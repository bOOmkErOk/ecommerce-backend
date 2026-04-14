

using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Domain.Entities.Products;

public class Rating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Value { get; set; }

    public Product Product { get; set; }
    public User User { get; set; }

}
