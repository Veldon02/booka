using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Services;

public interface IUserService
{
    Task<User?> GetByEmailAsync(string email);

    Task<User> AddAsync(User user);
}