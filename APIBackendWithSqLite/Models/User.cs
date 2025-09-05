using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;

public class User
{
    public int Id { get; set; } // Primary Key

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty; // Hashed password (not plain text)

    [MaxLength(100)]
    public string? FullName { get; set; }

    [MaxLength(255)]
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
