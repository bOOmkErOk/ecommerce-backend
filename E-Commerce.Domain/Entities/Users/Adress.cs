
namespace E_Commerce.Domain.Entities.Users;

public class Address
{
    public int Id { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string Apartment { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; }
}
