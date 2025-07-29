using Application.DTOs;
using Application.DTOs.Site;

public interface IAuthService
{
    Task<Guid> RegisterUserAsync(RegisterUserRequest request);
    Task<LoginUserResponse> LoginUserAsync(LoginUserDTO loginUserDto);
}


