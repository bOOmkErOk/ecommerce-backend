using E_Commerce.Application.Features.Auth.VerifyEmailChange;
using E_Commerce.Application.Features.Users.ChangePassword;
using E_Commerce.Application.Features.Users.UpdateUser;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Projection.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new UpdateUserCommand(

            request.Name,
            request.Email,
            request.PhoneNumber,
            request.CurrentPassword,
            userId
        );
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        if (request.NewPassword != request.ConfirmNewPassword)
            return BadRequest("Passwords do not match");

        var userId = GetCurrentUserId();
        var command = new ChangePasswordCommand(userId, request.CurrentPassword, request.NewPassword);
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("verify-email-change")]
    [Authorize]
    public async Task<IActionResult> VerifyEmailChange([FromBody] VerifyEmailChangeRequest request)
    {
        var userId = GetCurrentUserId();
        var command = new VerifyEmailChangeCommand(userId, request.Code);
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    public record VerifyEmailChangeRequest(string Code);
}
