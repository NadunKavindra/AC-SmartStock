using System.ComponentModel.DataAnnotations;

namespace APIBackend.DTOs;

public class RegisterUserDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public string? Email { get; set; }
}
