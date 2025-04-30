using Booka.Core.Domain;
using Booka.Core.Domain.enums.Workspace;
using Booka.Core.DTOs.Workspace;

namespace Booka.Core.Interfaces.Repositories;

public interface IWorkspaceRepository : IBaseRepository<Workspace, int>
{
    Task<Workspace?> GetByEmail(string email, bool asNoTracking = true);

    Task<(List<Workspace>, int)> Get(WorkspaceFilteringParamsDto filter, WorkspaceSorting sort);
}