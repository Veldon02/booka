using Booka.Application.Exceptions;
using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Interfaces.Services;
using Booka.Domain.Models;

namespace Booka.Application.Services;

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