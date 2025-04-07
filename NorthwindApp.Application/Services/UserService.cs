using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NorthwindApp.Domain;
using System.Security.Cryptography;

namespace NorthwindApp.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public UserService(IUserRepository userRepository,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<User> Add(User user, string password)
    {
        var passwordSalt = "";
        var passwordHash = _passwordService.GetHash(password, out passwordSalt);

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
}