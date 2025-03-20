using AutoMapper;
using Application.DTOs;
using webAPI.Domain.Models;
using Domain.Models;

namespace Application.Mappers;

public class ContactMapper : Profile
{
    public ContactMapper()
    {
        CreateMap<Contact, ContactDTO>();
    }
}
