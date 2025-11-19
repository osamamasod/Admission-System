using System.ComponentModel.DataAnnotations;

namespace AdmissionSystem.Core.Models;

public abstract class User : BaseEntity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }


    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }


    public string Role { get; set; } = string.Empty;

    
    public bool IsRefreshTokenValid()
    {
        return !string.IsNullOrEmpty(RefreshToken) && 
               RefreshTokenExpiryTime.HasValue && 
               RefreshTokenExpiryTime.Value > DateTime.UtcNow;
    }
}