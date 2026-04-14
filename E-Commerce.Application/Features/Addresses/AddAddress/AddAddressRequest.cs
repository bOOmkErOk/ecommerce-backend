
namespace E_Commerce.Application.Features.Addresses.AddAddress;

public record AddAddressRequest(
    string StreetAddress,
    string Apartment,
    string City
);
