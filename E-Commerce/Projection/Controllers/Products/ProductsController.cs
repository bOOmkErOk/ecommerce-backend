using E_Commerce.Application.Features.Products.AddProduct;
using E_Commerce.Application.Features.Products.RemoveProduct;
using E_Commerce.Application.Features.Products.SetSponsoredProduct;
using E_Commerce.Application.Features.Products.UpdatePrice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Projection.Controllers.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-product")]
    [Authorize]

    public async Task<IActionResult> AddProduct(AddProductRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new AddProductCommand(
            request.Title,
            request.Price,
            request.Description,
            request.ImageUrl,
            request.Category,
            userId
            );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }


    [HttpDelete("delete-product")]

    [Authorize]

    public async Task<IActionResult> RemoveProduct(RemoveProductRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new RemoveProductCommand(
            request.ProductId,
            userId
            );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);

    }

    [HttpPut("update-product")]
    [Authorize]

    public async Task<IActionResult> UpdateProductPrice(UpdatePriceRequest request)
    {
        var userId = GetCurrentUserId();

        var command = new UpdatePriceCommand(
            request.ProductId,
            request.Price,
            userId);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("set-sponsored")]
    [Authorize]
    public async Task<IActionResult> SetSponsored(SetSponsoredProductRequest request)
    {
        var command = new SetSponsoredProductCommand(
            request.ProductId,
            request.SponsoredUntil
        );
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }


}
