namespace AdmissionSystem.Core.Models;

public class AdmissionProgram : BaseEntity
{
    public int Priority { get; set; } // 1 = first choice, 2 = second choice, etc.
    
    // Foreign keys
    public int ApplicantAdmissionId { get; set; }
    public ApplicantAdmission ApplicantAdmission { get; set; } = null!;
    
    public int ProgramId { get; set; }
    public Program Program { get; set; } = null!;
}