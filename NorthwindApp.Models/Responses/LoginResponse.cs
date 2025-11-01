namespace NorthwindApp.Models;

public record LoginResponse(string Email, string AccessToken, int ExpiresIn);