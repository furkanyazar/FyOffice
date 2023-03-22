using Application.Services.Repositories;
using Core.Security.Entities;

namespace Application.Services.UserService;

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByEmail(string email)
    {
        User? user = await _userRepository.GetAsync(c => c.Email == email);
        return user;
    }

    public async Task<User?> GetById(int id)
    {
        User? user = await _userRepository.GetAsync(c => c.Id == id);
        return user;
    }
}