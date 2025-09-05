using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;

public class Logs
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(50)]
    public string Level { get; set; } = "Info"; // e.g., Info, Warning, Error

    [MaxLength(255)]
    public string? Source { get; set; } // Optional: e.g., "UserController", "OrderService"
    public string? Message { get; set; }
    public string? Exception { get; set; } // Optional: stack trace or exception message
}
