using E_Commerce.Application.Features.Ratings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Projection.Controllers.Ratings;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : BaseController
{
    private readonly IMediator _mediator;

    public RatingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("rate-product")]
    [Authorize]

    public async Task<IActionResult> RateProduct(ProductRatingRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new ProductRatingCommand(
            request.ProductId,
            request.Rating,
            userId);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

}
