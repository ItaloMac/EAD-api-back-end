using Application.DTOs;
using Application.DTOs.Site;
using Application.Interfaces;
using Domain.Models;
using InvictusAPI.jwt;
using Microsoft.EntityFrameworkCore;
using static Domain.Exceptions.CpfDuplicate;
using static Domain.Exceptions.EmailDuplicate;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context;
    private readonly TokenService _tokenService;

    public AuthService(IApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

   public async Task<LoginUserResponse> LoginUserAsync(LoginUserDTO loginUserDto)
{
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password))
            {
                throw new Exception("Email ou senha inválidos.");
            }

            if (!user.EmailConfirmed)
            {
                throw new Exception("Email não confirmado.");
            }

            var token = _tokenService.GenerateToken(user);
            var expiration = _tokenService.GetExpiration();

            return new LoginUserResponse(user.Id, token, expiration, userType: user.UserType);
        }

        catch (Exception)
        {
            throw;
        }
}

    public async Task<Guid> RegisterUserAsync(RegisterUserRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            throw new EmailDuplicateException("Email já cadastrado.");

        if (await _context.Users.AnyAsync(u => u.CPF == request.CPF))
            throw new CpfDuplicateException("CPF já cadastrado.");

        var newUser = new User
        {
            Email = request.Email,
            CPF = request.CPF,
            Name = request.Name,
            LastName = request.LastName,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
         _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser.Id;
    }
}




  