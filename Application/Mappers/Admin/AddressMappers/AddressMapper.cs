using System;
using AutoMapper;
using Application.DTOs.Admin.Address;

namespace Application.Mappers.Admin.AddressMappers;

public class AddressMapper : Profile
{
    public AddressMapper()
    {
        CreateMap<CreateAddressDTO, Address>();

        CreateMap<UpdateAddressDTO, Address>();

        CreateMap<Address, AddressResponseDTO>();

        CreateMap<Address, CreateAddressDTO>();

        CreateMap<Address, UpdateAddressDTO>();
    }
}
