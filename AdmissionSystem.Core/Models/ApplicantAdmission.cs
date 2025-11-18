using AdmissionSystem.Core.Models.Enums;

namespace AdmissionSystem.Core.Models;

public class ApplicantAdmission : BaseEntity
{
    public string ApplicationNumber { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public AdmissionStatus Status { get; set; } = AdmissionStatus.Draft;
    public string? Notes { get; set; }
    
    // Foreign keys
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;
    
    public int? ManagerId { get; set; }
    public Manager? Manager { get; set; }
    
    // Navigation properties
    public ICollection<AdmissionProgram> AdmissionPrograms { get; set; } = new List<AdmissionProgram>();
}