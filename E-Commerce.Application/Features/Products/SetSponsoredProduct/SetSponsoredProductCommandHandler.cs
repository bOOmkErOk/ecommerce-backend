using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Features.Products.SetSponsoredProduct;

public sealed class SetSponsoredProductCommandHandler : IRequestHandler<SetSponsoredProductCommand, Result<bool>>
{
    private readonly IDataContext _context;

    public SetSponsoredProductCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(SetSponsoredProductCommand command, CancellationToken ct)
    {
        var previousSponsored = await _context.Products
            .FirstOrDefaultAsync(p => p.IsSponsored, ct);

        if (previousSponsored != null)
        {
            previousSponsored.IsSponsored = false;
            previousSponsored.SponsoredUntil = null;
        }

        var product = await _context.Products.FindAsync(command.ProductId);
        if (product == null)
            return Result<bool>.Failure(Errors.ProductNotFound());

        product.IsSponsored = true;
        product.SponsoredUntil = command.SponsoredUntil;

        await _context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
