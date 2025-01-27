using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Repositories;

public interface IWorkplaceRepository : IBaseRepository<Workplace, int>
{
    Task<List<Workplace>> GetByWorkspace(int workspaceId);
}