namespace AdmissionSystem.Core.Models;

public class Program : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationInYears { get; set; }
    public decimal TuitionFee { get; set; }
    public int AvailableSeats { get; set; }
    public DateTime ApplicationDeadline { get; set; }
    
    // Foreign key
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    
    // Navigation properties
    public ICollection<AdmissionProgram> AdmissionPrograms { get; set; } = new List<AdmissionProgram>();
}