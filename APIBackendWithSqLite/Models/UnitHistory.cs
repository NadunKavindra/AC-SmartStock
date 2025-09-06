using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;

public class UnitHistory
{
    public int Id { get; set; }

    // Service - 1
    //      Normal Service - 11
    //      Indoor High preasure service - 12
    //      Dismantal service - 13
    // Repaire - 2
    // Breakdown - 3

    [Required] public int Type { get; set; } 
    public DateTime Date { get; set; }
    public string? Technician { get; set; }
    public string? ServiceType { get; set; }
    public string? Description { get; set; }
    public decimal InvoiceCost { get; set; }
    public decimal ActualCost { get; set; }
    public bool PaymentStatus { get; set; }

    public string? UnitIdCode { get; set; }
    public Unit? Unit { get; set; }
}
