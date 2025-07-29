using Domain.Models;

namespace Application.DTOs.Site;

public class LoginUserResponse
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public UserType UserType { get; set; }


    public LoginUserResponse(Guid userId, string token, DateTime expiration, UserType userType)
    {
        UserId = userId;
        Token = token;
        Expiration = expiration;
        UserType = userType;
    }
}
