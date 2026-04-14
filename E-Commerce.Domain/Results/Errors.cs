namespace E_Commerce.Domain.Results;

public sealed record Errors(int Status, string Message)
{
    // --- User Errors qvevitt ---

    public static Errors PhoneEmpty() =>
    new(400, "Phone number cannot be empty");

    public static Errors PhoneInvalid() =>
        new(400, "Invalid phone number format. Please enter a valid phone number (9-15 digits)");

    public static Errors PhoneExists() =>
        new(409, "This phone number is already registered to another account");

    public static Errors UserNotFound() =>
        new(404, "User not found");

    public static Errors UserByIdNotFound(int id) =>
        new(404, $"User with ID {id} not found");

    public static Errors EmailEmpty() =>
        new(400, "Email cannot be empty");

    public static Errors EmailInvalid() =>
        new(400, "Invalid email format. Please enter a valid email address");

    public static Errors EmailExists() =>
        new(409, "This email is already registered. Please use a different email");

    public static Errors UsernameEmpty() =>
        new(400, "Username cannot be empty");

    public static Errors UsernameInvalid() =>
        new(400, "Username must be 3-15 characters, only letters, numbers, and underscores");

    public static Errors UsernameExists() =>
        new(409, "This username is already taken. Please choose another");

    public static Errors PasswordEmpty() =>
        new(400, "Password cannot be empty");

    public static Errors PasswordWeak() =>
        new(400, "Password must be at least 8 characters with uppercase, lowercase, and numbers");

    public static Errors AgeInvalid() =>
        new(400, "Age must be between 13 and 120");

    // --- Authentication Errors qvevitt ---
    public static Errors Unauthorized() =>
        new(401, "Your session has expired. Please log in again");

    public static Errors InvalidCredentials() =>
        new(401, "Incorrect email or password. Please try again");
    public static Errors PasswordRequiredForEmailChange() =>
    new(400, "Please enter your current password to change email");

    public static Errors EmailSendFailed(string details) =>
        new(500, $"Failed to send verification email: {details}");

    public static Errors Forbidden() =>
        new(403, "You don't have permission to perform this action");

    public static Errors CodeExpired() =>
    new(400, "Verification code has expired");
    public static Errors InvalidCode() =>
        new(400, "Invalid verification code");
    public static Errors EmailNotVerified() =>
    new(403, "Please verify your email before logging in");

    // --- Generic Errors qvevitt ---
    public static Errors ValidationFailed() =>
        new(400, "Validation failed. Please check your input and try again");

    public static Errors NotFound() =>
        new(404, "The requested resource was not found");

    public static Errors ServerError() =>
        new(500, "An unexpected error occurred. Please try again later");

    // --- Product Errors ---
    public static Errors ProductNotFound() =>
        new(404, "Product not found");
    public static Errors ProductByIdNotFound(int id) =>
        new(404, $"Product with ID {id} not found");
    public static Errors ProductNameEmpty() =>
        new(400, "Product name cannot be empty");
    public static Errors ProductPriceInvalid() =>
        new(400, "Product price must be greater than 0");
    public static Errors ProductStockInvalid() =>
        new(400, "Product stock cannot be negative");
    public static Errors ProductOutOfStock() =>
        new(400, "Product is out of stock");

    // --- Cart Errors ---
    public static Errors CartNotFound() =>
        new(404, "Cart not found");
    public static Errors CartItemNotFound() =>
        new(404, "Cart item not found");
    public static Errors CartEmpty() =>
        new(400, "Your cart is empty");
    public static Errors InvalidQuantity() =>
        new(400, "Quantity must be at least 1");

    // --- Order Errors ---
    public static Errors OrderNotFound() =>
        new(404, "Order not found");
    public static Errors OrderAlreadyCancelled() =>
        new(400, "This order has already been cancelled");
    public static Errors OrderCannotBeCancelled() =>
        new(400, "Order cannot be cancelled at this stage");

    // --- Rating Errors ---
    public static Errors RatingNotFound() =>
        new(404, "Rating not found");

    public static Errors NotPurchased() =>
    new(403, "You can only rate products you have purchased");

    public static Errors RatingInvalid() =>
        new(400, "Rating must be between 1 and 5");

    public static Errors AlreadyRated() =>
        new(409, "You have already rated this product");
}
