
using Application.DTOs.Admin.User;

namespace Application.Interfaces.Admin;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO?>> GetAllUsers();
    Task<UserResponseDTO?> GetUserById(Guid id);
    Task<UserResponseDTO> CreateUserAsync(UserResponseDTO user);
    Task<UserResponseDTO> UpdateUserAsync(Guid id, UserUpdateDTO user);
    Task<UserResponseDTO> DeleteUserAsync(Guid id);
    Task<List<UserRegistrationDTO>> GetUserRegistrationsByUserIdAsync(Guid userId);
}
