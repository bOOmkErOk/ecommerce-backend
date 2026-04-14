

namespace E_Commerce.Domain.Common;

public abstract class Verification
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(15);
}
