using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;

public class BillTest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TestID { get; set; } // Primary Key
    public int BillId { get; set; }

    [Required]
    public int TestCode { get; set; }

    [Required]
    [MaxLength(100)]
    public string TestName { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }
    public bool IsExternalTest { get; set; }
    public string? ExternalLab { get; set; } = string.Empty;
    public string TestCategory { get; set; } = string.Empty; // e.g., Hematology, Biochemistry
    public string Description { get; set; } = string.Empty;

    // Navigation Property
    public BillDetails BillDetails { get; set; }
}
