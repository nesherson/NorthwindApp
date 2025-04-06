using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NorthwindApp.Domain;
using System.Security.Cryptography;

namespace NorthwindApp.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Add(User user, string password)
    {
        var passwordSalt = "";
        var passwordHash = GetHash(password, out passwordSalt);

        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;

        _userRepository.Add(user);
        await _userRepository.SaveChanges();

        return await GetById(user.Id);
    }

    public async Task<User?> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<User?> GetById(int id, string? includes = null)
    {
        return await _userRepository.GetById(id, includes);
    }

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