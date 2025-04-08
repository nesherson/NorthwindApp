using NorthwindApp.Domain;

namespace NorthwindApp.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository,
        IPasswordHasher passwordService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordService;
    }

    public async Task<User> Add(User user, string password)
    {
        user.PasswordSalt = _passwordHasher.GenerateSalt(); ;
        user.PasswordHash = _passwordHasher.ComputeHash(password, user.PasswordSalt);

        _userRepository.Add(user);
        await _userRepository.SaveChanges();

        return await GetById(user.Id);
    }

    public async Task Update(User user, string password)
    {
        user.PasswordSalt = _passwordHasher.GenerateSalt();
        user.PasswordHash = _passwordHasher.ComputeHash(password, user.PasswordSalt);

        _userRepository.Update(user);
        await _userRepository.SaveChanges();
    }

    public async Task<User?> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _userRepository.GetByEmail(email);
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