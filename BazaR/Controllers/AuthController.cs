using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;

public record GoogleAuthDto(string IdToken);

[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("/auth/google")]
    public async Task<IActionResult> Google([FromBody] GoogleAuthDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.IdToken))
            return BadRequest(new { message = "Missing idToken" });

        GoogleJsonWebSignature.Payload payload;

        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[]
                {
                    "596714054566-thnvk9mt56pa9fr64escum0ucj1hsr9b.apps.googleusercontent.com"
                }
            });
        }
        catch
        {
            return Unauthorized(new { message = "Invalid Google token" });
        }

        if (payload.Email is null || payload.EmailVerified != true)
            return Unauthorized(new { message = "Email not verified" });
        return Ok(new { redirectUrl = "/" });
    }
}