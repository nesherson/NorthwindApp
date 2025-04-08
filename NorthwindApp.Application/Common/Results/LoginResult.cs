namespace NorthwindApp.Application;

public class LoginResult
{
    public LoginResult(string message, bool isSuccess)
    {
        Message = message;
        IsSuccess = isSuccess;
    }

    public LoginResult(string message, bool isSuccess, string token)
    {
        Message = message;
        IsSuccess = isSuccess;
        Token = token;
    }

    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public string? Token { get; set; }
}
