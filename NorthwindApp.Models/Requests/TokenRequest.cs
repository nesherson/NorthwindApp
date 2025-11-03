namespace NorthwindApp.Models;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);