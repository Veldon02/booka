using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User, int>
{
    Task<User?> GetByEmail(string email, bool asNoTracking = true);
}