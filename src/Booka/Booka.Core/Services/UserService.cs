using Booka.Core.Domain;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Azure;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Services;

namespace Booka.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailNotificationService _emailNotificationService;

    public UserService(IUserRepository userRepository, IEmailNotificationService emailNotificationService)
    {
        _userRepository = userRepository;
        _emailNotificationService = emailNotificationService;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var result = await _userRepository.GetByEmail(email);

        return result;
    }

    public async Task<User> AddAsync(User user)
    {
        user.Email = user.Email.ToLowerInvariant();

        user = await _userRepository.Add(user);

        await _emailNotificationService.PushUserRegistrationMessage(user.Email, user.FirstName);

        return user;
    }
}