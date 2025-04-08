namespace NorthwindApp.Application;

public interface IPasswordHasher
{
    string ComputeHash(string password, string salt);

    string GenerateSalt();
}
