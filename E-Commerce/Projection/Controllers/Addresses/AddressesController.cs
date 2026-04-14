using E_Commerce.Application.Features.Addresses.AddAddress;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Projection.Controllers.Addresses;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : BaseController
{
    private readonly IMediator _mediator;

    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-address")]
    [Authorize]
    public async Task<IActionResult> AddAddress(AddAddressRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new AddAddressCommand(
            request.StreetAddress,
            request.Apartment,
            request.City,
            userId
        );
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
