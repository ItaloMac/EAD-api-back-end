using Application.DTOs;

namespace Application.Interfaces;

public interface IContactService
{
    public Task<ContactDTO> PostContactAsync(string Name, string Email, string Phone);
}
