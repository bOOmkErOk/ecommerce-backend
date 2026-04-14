using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Entities.Carts;
using E_Commerce.Domain.Enums.Users;
namespace E_Commerce.Domain.Entities.Users;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;

    public EmailVerification? EmailVerification { get; set; }
    public bool IsEmailVerified { get; set; } = false;
    public string? PendingEmail { get; set; }

    public List<Address> Addresses { get; set; } = new List<Address>();
    public Cart Cart { get; set; }

    public UserRoles Role { get; set; }
}
