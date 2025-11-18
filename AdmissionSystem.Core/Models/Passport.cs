using AdmissionSystem.Core.Models.Enums;

namespace AdmissionSystem.Core.Models;

public class Passport : Document
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}