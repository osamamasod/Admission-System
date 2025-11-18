using AdmissionSystem.Core.Models.Enums;

namespace AdmissionSystem.Core.Models;

public abstract class Document : BaseEntity
{
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string IssuingAuthority { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    
    // Foreign key to Applicant
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;
}