
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ResetPassowordDTO
{
    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string resetCode { get; set; } = null!;

    [Required]
    public string newPassword { get; set; } = null!;
}
