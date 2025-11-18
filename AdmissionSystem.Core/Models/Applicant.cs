using AdmissionSystem.Core.Models.Enums;

namespace AdmissionSystem.Core.Models;

public class Applicant : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Nationality { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<ApplicantAdmission> Admissions { get; set; } = new List<ApplicantAdmission>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    // REMOVE THESE TWO LINES:
    // public Passport? Passport { get; set; }
    // public ICollection<EducationDocument> EducationDocuments { get; set; } = new List<EducationDocument>();
}