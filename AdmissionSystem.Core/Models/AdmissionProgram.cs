namespace AdmissionSystem.Core.Models;

public class AdmissionProgram : BaseEntity
{
    public int Priority { get; set; } 
    
    public int ApplicantAdmissionId { get; set; }
    public ApplicantAdmission ApplicantAdmission { get; set; } = null!;
    
    public int ProgramId { get; set; }
    public Program Program { get; set; } = null!;
}