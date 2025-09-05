using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;
public class Product
{
    public int Id { get; set; } // Primary Key

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
