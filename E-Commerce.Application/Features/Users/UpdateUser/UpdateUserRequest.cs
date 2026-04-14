namespace E_Commerce.Application.Features.Users.UpdateUser;

public record UpdateUserRequest(
    string Name,
    string Email,
    string PhoneNumber,
    string? CurrentPassword
);
