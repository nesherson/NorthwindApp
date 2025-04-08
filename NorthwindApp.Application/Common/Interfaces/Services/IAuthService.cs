namespace NorthwindApp.Application;

public interface IAuthService
{
    Task<LoginResult> LogInUser(string email, string password);
}
