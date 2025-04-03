using NorthwindApp.Domain;

namespace NorthwindApp.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;    
    }
    public async Task Add(User user)
    {
        _userRepository.Add(user);
        await _userRepository.SaveChanges();
    }

    public async Task<User?> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }
}
