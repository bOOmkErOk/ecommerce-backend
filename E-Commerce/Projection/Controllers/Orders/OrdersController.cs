using E_Commerce.Application.Features.Orders.CancelOrder;
using E_Commerce.Application.Features.Orders.CreateOrder;
using E_Commerce.Application.Features.Orders.UpdateStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Projection.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-order")]
    [Authorize]
    public async Task<IActionResult> CreateOrder()
    {
        var userId = GetCurrentUserId();

        var command = new CreateOrderCommand(userId);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPut("cancel-order")]
    [Authorize]
    public async Task<IActionResult> CancelOrder(CancelOrderRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new CancelOrderCommand(userId,
            request.OrderId
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPut("update-status")]
    [Authorize]
    public async Task<IActionResult> UpdateStatus(UpdateStatusRequest request)
    {

        var command = new UpdateStatusCommand(
            request.OrderId,
            request.NewStatus
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
