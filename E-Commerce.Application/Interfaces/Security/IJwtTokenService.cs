

using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Application.Interfaces.Security;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
}