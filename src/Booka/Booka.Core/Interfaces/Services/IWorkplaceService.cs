using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Services;

public interface IWorkplaceService
{
    Task<List<Workplace>> GetByWorkspace(int workspaceId);

    Task<Workplace> CreateWorkplace(int workspaceId, Workplace workplace);
}