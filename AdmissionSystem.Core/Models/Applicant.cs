using AdmissionSystem.Core.Models.Enums;

namespace AdmissionSystem.Core.Models;

public class Applicant : User
{
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Nationality { get; set; } = string.Empty;
    
   
    public ICollection<ApplicantAdmission> Admissions { get; set; } = new List<ApplicantAdmission>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}