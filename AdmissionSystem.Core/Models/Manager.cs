namespace AdmissionSystem.Core.Models;

public class Manager : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    
    // Foreign key
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    
    // Navigation properties
    public ICollection<ApplicantAdmission> ManagedAdmissions { get; set; } = new List<ApplicantAdmission>();
}