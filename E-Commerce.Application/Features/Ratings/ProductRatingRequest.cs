namespace E_Commerce.Application.Features.Ratings;

public record ProductRatingRequest
(
    int ProductId,
    int Rating
    );