namespace NorthwindApp.Models;

public class AuthResponse
{
    public required AuthUserResponse User { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime AccessTokenExpiration { get; set; }
}