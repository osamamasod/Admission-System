using AdmissionSystem.Core.DTOs.Auth;
using AdmissionSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var result = await _authService.RegisterApplicantAsync(request);
            return Ok(new { message = "Registration successful" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception )
        {
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }
        catch (Exception )
        {
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }

   // AdmissionSystem.API/Controllers/AuthController.cs
[HttpPost("refresh-token")]
public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] string refreshToken)
{
    try
    {
        var result = await _authService.RefreshTokenAsync(refreshToken);
        return Ok(result);
    }
    catch (UnauthorizedAccessException)
    {
        return Unauthorized(new { message = "Invalid refresh token" });
    }
    catch (Exception)
    {
        return StatusCode(500, new { message = "An error occurred while refreshing token" });
    }
}

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new { message = "Unable to identify user" });
        }

        await _authService.LogoutAsync(email);
        return Ok(new { message = "Logout successful" });
    }
}