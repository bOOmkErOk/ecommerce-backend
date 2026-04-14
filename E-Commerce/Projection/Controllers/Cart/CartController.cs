using E_Commerce.Application.Features.Cart.AddItem;
using E_Commerce.Application.Features.Cart.RemoveItem;
using E_Commerce.Application.Features.Cart.UpdateQuantity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Projection.Controllers.Cart;

[Route("api/[controller]")]
[ApiController]
public class CartController : BaseController
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-item")]
    [Authorize]

    public async Task<IActionResult> AddItem(AddItemRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new AddItemCommand(
            request.ProductId,
            request.Quantity,
            userId
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);

    }

    [HttpDelete("remove-product")]
    [Authorize]

    public async Task<IActionResult> RemoveItem(RemoveItemRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new RemoveItemCommand(
            request.CartItemId,
            userId
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPut("update-quantity")]
    [Authorize]
    public async Task<IActionResult> UpdateItemQuantity(UpdateQuantityRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new UpdateQuantityCommand(
            request.ItemId,
            request.Quantity,
            userId
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
