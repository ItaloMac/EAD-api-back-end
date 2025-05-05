using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class LogoutDTO
{
    [Required(ErrorMessage = "O refresh token é obrigatório")]
    public string RefreshToken { get; set; } = string.Empty;
}
