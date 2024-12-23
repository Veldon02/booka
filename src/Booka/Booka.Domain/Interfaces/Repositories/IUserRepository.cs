using Booka.Domain.Models;

namespace Booka.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User, int>
{
    Task<User?> GetByEmail(string email, bool asNoTracking = true);
}