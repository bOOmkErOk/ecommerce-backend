using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Ratings;

public record ProductRatingCommand
(

    int ProductId,
    int Rating,
    int UserId
    ) : IRequest<Result<int>>;
