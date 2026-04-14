using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Users;
namespace E_Commerce.Domain.Entities.Auth;

public sealed class EmailVerification : Verification
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public static EmailVerification Create()
    {
        Random random = new();
        string code = random.Next(100_000, 999_999).ToString();
        return new EmailVerification() { Code = code };
    }

    public void SetUserId(int userId) => UserId = userId;
}
