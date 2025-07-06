using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ForgotPasswordDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;
}
