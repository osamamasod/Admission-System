namespace AdmissionSystem.Core.Models;

public class FileEntity : BaseEntity
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }

    public int? ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }
    
    public int? DocumentId { get; set; }
    public Document? Document { get; set; }
}