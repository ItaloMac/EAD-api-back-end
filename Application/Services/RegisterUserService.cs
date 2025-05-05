using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

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
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Erro ao registrar usuário: {errors}");
        }

        return user.Id;
    }
}
