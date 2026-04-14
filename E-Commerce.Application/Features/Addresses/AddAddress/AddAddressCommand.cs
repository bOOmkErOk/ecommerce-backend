
using E_Commerce.Domain.Results;
using MediatR;

namespace E_Commerce.Application.Features.Addresses.AddAddress;

public record AddAddressCommand
(
       string StreetAddress,
    string Apartment,
    string City,
    int UserId
    ) : IRequest<Result<int>>;
