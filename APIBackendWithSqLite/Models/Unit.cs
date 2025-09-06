using System.ComponentModel.DataAnnotations;

namespace APIBackend.Models;

public class Unit
{
    [Required] public string UnitIdCode { get; set; } = string.Empty;
    [Required] public string UnitName { get; set; } = string.Empty;
    public string? UnitDescription { get; set; }
    public string? Capacity { get; set; }
    public DateTime InstallationDate { get; set; }
    public string? Description { get; set; }
    public DateTime ServiceDueDate { get; set; }

    // Foreign Key as string
    public int LocationId { get; set; }
    public Location? Location { get; set; }

    // One Unit has many histories
    public ICollection<UnitHistory>? UnitHistories { get; set; }

}
