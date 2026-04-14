using E_Commerce.Application.Features.WishList;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Projection.Controllers.Wishlist;

[Route("api/[controller]")]
[ApiController]
public class WishlistController : BaseController
{
    private readonly IMediator _mediator;

    public WishlistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("toggle")]
    public async Task<IActionResult> ToggleWishlist([FromBody] ToggleWishlistRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new ToggleWishlistCommand(userId, request.ProductId);

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
