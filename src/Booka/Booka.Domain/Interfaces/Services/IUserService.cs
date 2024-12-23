using Booka.Domain.Models;

namespace Booka.Domain.Interfaces.Services;

public interface IUserService
{
    Task<User> GetByEmailAsync(string email);

    Task<User> AddAsync(User user);
}