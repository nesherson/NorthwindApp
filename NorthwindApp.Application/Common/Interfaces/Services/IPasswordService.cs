namespace NorthwindApp.Application;

public interface IPasswordService
{
    string GetHash(string password, out string passwordSalt);
}
