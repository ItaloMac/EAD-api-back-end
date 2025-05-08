using Application.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Domain.Exceptions.CpfDuplicate;
using static Domain.Exceptions.EmailDuplicate;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;

      public IdentityService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Guid> RegisterUserAsync(RegisterUserRequest request) //RegisterUserRequest => DTO
    {
        var user = new User
        {
            UserName = request.Email, // obrigatório para o Identity funcionar
            Email = request.Email,
            CPF = request.CPF,
            Name = request.Name,
            LastName = request.LastName,
        };

        if (await _userManager.FindByEmailAsync(request.Email) != null)
            throw new EmailDuplicateException("Email já cadastrado.");

        if (await _userManager.Users.AnyAsync(u => u.CPF ==request.CPF))
            throw new CpfDuplicateException("CPF já cadastrado.");

        var result = await _userManager.CreateAsync(user, request.Password);

        return user.Id;
    }
}
