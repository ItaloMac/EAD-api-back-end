using System.Globalization;
using Application.DTOs.Admin.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Application.Services.Admin.UserServices;

public class UserService : Interfaces.Admin.IUserService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserResponseDTO> CreateUserAsync(UserResponseDTO user)
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                throw new Exception("E-mail já cadastrado.");
            }

            if (await _context.Users.AnyAsync(u => u.CPF == user.CPF))
            {
                throw new Exception("CPF já cadastrado.");
            }

            var newUser = _mapper.Map<User>(user);
            newUser.BirthDate = string.IsNullOrWhiteSpace(user.BirthDate)
                ? null
                : DateTime.ParseExact(user.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponseDTO>(result.Entity);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao criar usuário.", ex);
        }
    }
    

    public async Task<IEnumerable<UserResponseDTO?>> GetAllUsers()
    {

        try
        {
            var users = await _context.Users
                .ProjectTo<UserResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

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
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
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
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
                throw new Exception("Usuário não encontrado.");

            if (await _context.Users.Where(m => m.Email == existingUser.Email && m.Id != id).AnyAsync())
            {
                throw new Exception("E-mail já cadastrado.");
            }

            if (await _context.Users.Where(c => c.CPF == existingUser.CPF && c.Id != id).AnyAsync())
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

            var result = _context.Users.Update(existingUser);

            await _context.SaveChangesAsync();

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
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new Exception("Usuário não encontrado.");

            var deleteUser = await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();
            return _mapper.Map<UserResponseDTO>(user);
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
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

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
