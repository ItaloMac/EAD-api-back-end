using System.Globalization;
using Application.DTOs.Admin.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Application.Services.Admin.UserServices;

public class UserService : Interfaces.Admin.IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public Task<UserResponseDTO> CreateUserAsync(UserResponseDTO user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserResponseDTO?>> GetAllUsers()
    {

        try
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null || !users.Any())
                throw new Exception("Nenhum usuário encontrado.");

            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar usuários.", ex);
        }
    }

    public async Task<UserResponseDTO?> GetUserById(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            return _mapper.Map<UserResponseDTO>(user);
        }
        catch
        {
            throw new ApplicationException("Erro ao buscar usuário.");
        }
    }

    public async Task<UserResponseDTO> UpdateUserAsync(Guid id, UserUpdateDTO user)
    {
        try
        {
            var existingUser = _userManager.FindByIdAsync(id.ToString()).Result;
            if (existingUser == null)
                throw new Exception("Usuário não encontrado.");

            if (await _userManager.FindByEmailAsync(user.Email) != null &&
              existingUser.Email != user.Email)
            {
                throw new Exception("E-mail já cadastrado.");
            }

            if (await _userManager.Users.AnyAsync(u => u.CPF == user.CPF && u.Id != id))
            {
                throw new Exception("CPF já cadastrado.");
            }

            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.CPF = user.CPF;
            existingUser.BirthDate = string.IsNullOrWhiteSpace(user.BirthDate)
                ? null
                : DateTime.ParseExact(user.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            existingUser.ProfilePhoto = user.ProfilePhoto;
            existingUser.UserType = user.UserType ?? existingUser.UserType;
            existingUser.VindiCustomerId = user.VindiCustomerId;

            var result = _userManager.UpdateAsync(existingUser).Result;

            if (!result.Succeeded)
                throw new Exception("Erro ao atualizar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return await Task.FromResult(_mapper.Map<UserResponseDTO>(existingUser));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar usuário.", ex);
        }
    }

    public async Task<UserResponseDTO> DeleteUserAsync(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            var deleteUser = await _userManager.DeleteAsync(user);
            if (!deleteUser.Succeeded)
                throw new Exception("Erro ao deletar usuário: " + string.Join(", ", deleteUser.Errors.Select(e => e.Description)));

            var userDto = _mapper.Map<UserResponseDTO>(user);
            return userDto;
            
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao deletar usuário.", ex);
        }
    }

    public async Task<List<UserRegistrationDTO>> GetUserRegistrationsByUserIdAsync(Guid userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            var registrations = await _context.Registrations
                .Include (r => r.Class)
                    .ThenInclude(c => c.Curso)
                .Where(r => r.UserId == user.Id)
                .ProjectTo<UserRegistrationDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return registrations;

        }
        catch(Exception ex)
        {
            throw new ApplicationException("Erro ao listar as matriculas do usuario", ex);
        }
    }
}
