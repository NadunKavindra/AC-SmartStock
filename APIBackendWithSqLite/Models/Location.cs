using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBackend.Models;

public class Location
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LocationId { get; set; }
    [Required] public string LocationName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime CreatedDate { get; set; }

    // Foreign Key as string
    public string? CustomerIdCode { get; set; }
    public Customer? Customer { get; set; }

    //Navigation property - One location has many units
    public ICollection<Unit>? Units { get; set; }
}
