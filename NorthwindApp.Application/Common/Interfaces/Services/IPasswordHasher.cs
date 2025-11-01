namespace NorthwindApp.Application;

public interface IPasswordHasher
{
    string ComputeHash(string password, string salt);
    string GenerateSalt();
    bool VerifyPassword(string existingPasswordHash, string newPassword, string salt);
}
