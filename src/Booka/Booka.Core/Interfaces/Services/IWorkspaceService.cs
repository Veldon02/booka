using Booka.Core.Domain;
using Booka.Core.Domain.enums.Workspace;
using Booka.Core.DTOs.Common;
using Booka.Core.DTOs.Workspace;

namespace Booka.Core.Interfaces.Services;

public  interface IWorkspaceService
{
    Task<Workspace> AddAsync(Workspace workspace);

    Task<List<Workspace>> GetAllAsync();

    Task UpdateAsync(int workspaceId, WorkspaceDto workspaceDto);

    Task<Workspace> GetByIdAsync(int workspaceId);

    Task<PagedCollection<Workspace>> Get(WorkspaceFilteringParamsDto filter, WorkspaceSorting sort);
}