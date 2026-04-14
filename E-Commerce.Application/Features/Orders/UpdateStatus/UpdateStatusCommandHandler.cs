
using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Orders.UpdateStatus;

public sealed class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public UpdateStatusCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateStatusCommand command, CancellationToken ct)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == command.OrderId);

        if (order == null)
        {
            return Result<bool>.Failure(Errors.OrderNotFound());
        }

        order.Status = command.NewStatus;
        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
