
using System.ComponentModel.DataAnnotations;

public class CreateContactRequest
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;
}