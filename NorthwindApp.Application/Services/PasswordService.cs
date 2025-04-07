using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace NorthwindApp.Application;

public class PasswordService : IPasswordService
{
    public string GetHash(string password, out string passwordSalt)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        passwordSalt = Convert.ToBase64String(salt);

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}