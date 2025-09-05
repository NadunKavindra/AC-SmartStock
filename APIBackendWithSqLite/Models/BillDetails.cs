using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;

public class BillDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BillId { get; set; }
    public int ReceiptNumber { get; set; }
    public string BillDateId { get; set; } = string.Empty;
    public DateTime BillDate { get; set; }
    public string ServedBy { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? TenderedAmount { get; set; }
    public decimal? Change { get; set; }
    public string? Remarks { get; set; }
    public bool IsBillPayed { get; set; } = false;

    // Navigation Property
    public ICollection<BillTest> BillTests { get; set; } = new List<BillTest>(); // Related tests.
}
