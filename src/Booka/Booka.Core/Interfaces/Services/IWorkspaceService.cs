using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Services;

public  interface IWorkspaceService
{
    Task<Workspace> AddAsync(Workspace workspace);

    Task<List<Workspace>> GetAllAsync();

    Task UpdateAsync(int workspaceId, Workspace workspace);

    Task<Workspace> GetByIdAsync(int workspaceId);
}