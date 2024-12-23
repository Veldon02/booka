using Booka.Core.Domain;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Security;
using Microsoft.EntityFrameworkCore;

namespace Booka.Infrastructure.Database.Repositories;

public class UserRepository : BaseRepository<User, int>, IUserRepository
{
    private readonly IHasher _hasher;

    public UserRepository(BookaDbContext dbContext, IHasher hasher)
        : base(dbContext)
    {
        _hasher = hasher;
    }

    public async Task<User?> GetByEmail(string email, bool asNoTracking = true)
    {
        var query = asNoTracking ? dbSet.AsNoTracking() : dbSet;

        return await query.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> Add(User entity)
    {
        entity.Password = _hasher.Hash(entity.Password);
        return await base.Add(entity);
    }
}