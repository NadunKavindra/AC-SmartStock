using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;

public class Customer
{
    [Required] public string CustomerIdCode { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation property - One customer can have many locations
    public ICollection<Location>? Locations { get; set; }

}
