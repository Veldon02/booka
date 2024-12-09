using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Models;

namespace Booka.Infrastructure.Database.Repositories;

public class UserRepository : BaseRepository<User, int>, IUserRepository
{
    public UserRepository(BookaDbContext dbContext) : base(dbContext)
    {
    }
}