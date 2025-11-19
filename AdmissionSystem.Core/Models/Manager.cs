namespace AdmissionSystem.Core.Models;

public class Manager : User
{
   
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    
 
    public ICollection<ApplicantAdmission> ManagedAdmissions { get; set; } = new List<ApplicantAdmission>();
}