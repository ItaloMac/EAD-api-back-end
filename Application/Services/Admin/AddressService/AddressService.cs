using Application.DTOs.Admin.Address;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;

namespace Application.Services.Admin.AddressService;

public class AddressService : IAddressService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddressService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateAddressDTO> CreateAddressAsync(CreateAddressDTO createAddressDTO)
    {
        try
        {
            var address = _mapper.Map<Address>(createAddressDTO);
            address.Id = Guid.NewGuid(); // Gerar ID explicitamente para ser usado como referência
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateAddressDTO>(address);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao criar o endereço.", ex);
        }
    }

    public Task<AddressResponseDTO> GetAddressByIdAsync(Guid id)
    {
        try
        {
            var address = _context.Addresses.Find(id);
            if (address == null)
            {
                throw new Exception("Endereço não encontrado.");
            }
            var addressResponseDTO = _mapper.Map<AddressResponseDTO>(address);
            return Task.FromResult(addressResponseDTO);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao buscar o endereço.", ex);
        }
    }

    public async Task<UpdateAddressDTO> UpdateAddressAsync(Guid id, UpdateAddressDTO updateAddressDTO)
    {
        try
        {
            var address = _context.Addresses.Find(id);
            if (address == null)
            {
                throw new Exception("Endereço não encontrado.");
            }

            // Manter o ID original
            updateAddressDTO.Id = id;
            _mapper.Map(updateAddressDTO, address);
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateAddressDTO>(address);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao atualizar o endereço.", ex);
        }
    }
}
