using AdmissionSystem.Core.Models.Enums;

namespace AdmissionSystem.Core.Models;

public class EducationDocument : Document
{
    public EducationDocumentType DocumentType { get; set; }
    public EducationLevel EducationLevel { get; set; }
    public string InstitutionName { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public decimal GPA { get; set; }
    public int GraduationYear { get; set; }
}