namespace NorthwindApp.Models;

public record TokenResponse(string Email, string AccessToken, int ExpiresIn);