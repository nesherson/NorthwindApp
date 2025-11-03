using NorthwindApp.Common;

namespace NorthwindApp.Models.Errors;

public static class AuthenticationErrors
{
    public static readonly Error InvalidOrExpiredAuthenticationCode = Error.Unauthorized("Authentication.Problem", "Invalid or expired authorization code.");
    public static readonly Error UserNotFound = Error.Unauthorized("Authentication.Problem", "User not found.");
    public static readonly Error ClaimNotFound = Error.Unauthorized("Authentication.Problem", "Claim not found.");
    public static readonly Error ActionHandlerNotFound = Error.Problem("Authentication.Problem", "Action handler not found.");
    public static readonly Error ExternalLoginInfoIsNull = Error.Problem("Authentication.Problem", "External login info is null.");
    public static readonly Error IncorrectPassword = Error.Problem("Authentication.Problem", "Password is incorrect.");
    public static readonly Error InvalidAccessToken = Error.Problem("Authentication.Problem", "Invalid access token.");
    public static readonly Error InvalidRefreshToken = Error.Problem("Authentication.Problem", "Invalid refresh token.");
}
