using Booka.Core.Domain;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Services;

namespace Booka.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmail(email)
            ?? throw new NotFoundException($"The user {email} is not found");
    }

    public async Task<User> AddAsync(User user)
    {
        user.Email = user.Email.ToLowerInvariant();

        return await _userRepository.Add(user);
    }
}