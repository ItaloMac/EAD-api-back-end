using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Application.Mappers;

namespace Application.Services;

public class ContactService : IContactService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ContactService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContactDTO> PostContactAsync(string Name, string Email, string Phone)
    {
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Email = Email,
            Phone = Phone
        };

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return _mapper.Map<ContactDTO>(contact);
    }
}
