
using AdmissionSystem.Core.DTOs.Auth;

namespace AdmissionSystem.Core.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<bool> RegisterApplicantAsync(RegisterRequest request);
    Task<LoginResponse> RefreshTokenAsync(string refreshToken); // Keep as string
    Task<bool> LogoutAsync(string email);
}