using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Repositories;

public interface IWorkspaceRepository : IBaseRepository<Workspace, int>
{
    Task<Workspace?> GetByEmail(string email, bool asNoTracking = true);
}