namespace AdmissionSystem.Core.Models;

public class Faculty : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<Program> Programs { get; set; } = new List<Program>();
    public ICollection<Manager> Managers { get; set; } = new List<Manager>();
}