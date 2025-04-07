﻿using NorthwindApp.Domain;

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

    public async Task Update(User user, string password)
    {
        var passwordSalt = "";
        var passwordHash = _passwordService.GetHash(password, out passwordSalt);

        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;

        _userRepository.Update(user);
        await _userRepository.SaveChanges();
    }

    public async Task<User?> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<User?> GetById(int id, string? includes = null)
    {
        return await _userRepository.GetById(id, includes);
    }

    public async Task<List<User>> Get(UserQueryObject query)
    {
       return await _userRepository.Get(query);
    }

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
        await _userRepository.SaveChanges();
    }
}