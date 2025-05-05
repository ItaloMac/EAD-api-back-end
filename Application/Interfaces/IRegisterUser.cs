using Application.DTOs;

public interface IIdentityService
{
    Task<Guid> RegisterUserAsync(RegisterUserRequest request);
}

