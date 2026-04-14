
using E_Commerce.Application.Interfaces.Persistence;
using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Addresses.AddAddress;

public sealed class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, Result<int>>
{
    private readonly IDataContext _context;

    public AddAddressCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(AddAddressCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync(command.UserId);
        if (user == null)
            return Result<int>.Failure(Errors.UserNotFound());

        var address = new Address
        {
            StreetAddress = command.StreetAddress,
            Apartment = command.Apartment,
            City = command.City,
            UserId = command.UserId
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync(ct);
        return Result<int>.Success(address.Id);
    }
}
