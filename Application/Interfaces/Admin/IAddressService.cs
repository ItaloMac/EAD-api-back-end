using Application.DTOs.Admin.Address;

namespace Application.Interfaces.Admin;

public interface IAddressService
{
    Task<AddressResponseDTO> GetAddressByIdAsync(Guid id);
    Task<CreateAddressDTO> CreateAddressAsync(CreateAddressDTO createAddressDTO);
    Task<UpdateAddressDTO> UpdateAddressAsync(Guid id, UpdateAddressDTO updateAddressDTO);
}
