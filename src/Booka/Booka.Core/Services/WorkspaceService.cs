using Booka.Core.Domain;
using Booka.Core.Domain.enums.Workspace;
using Booka.Core.DTOs.Common;
using Booka.Core.DTOs.Workspace;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Services;

namespace Booka.Core.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly IWorkspaceRepository _workspaceRepository;

    public WorkspaceService(IWorkspaceRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Workspace> AddAsync(Workspace workspace)
    {
        workspace.Email = workspace.Email.ToLowerInvariant();

        return await _workspaceRepository.Add(workspace);
    }

    public async Task<List<Workspace>> GetAllAsync()
    {
        return await _workspaceRepository.GetAll();
    }

    public async Task UpdateAsync(int workspaceId, Workspace workspace)
    {
        var existingWorkspace = await _workspaceRepository.GetById(workspaceId, false)
                                ?? throw new NotFoundException($"Workspace {workspaceId} is not found");

        existingWorkspace.Name = workspace.Name;
        existingWorkspace.Address = workspace.Address;
        
        await _workspaceRepository.Update(existingWorkspace);
    }

    public async Task<Workspace> GetByIdAsync(int workspaceId)
    {
        return await _workspaceRepository.GetById(workspaceId) 
               ?? throw new NotFoundException($"Workspace {workspaceId} is not found");
    }

    public async Task<PagedCollection<Workspace>> Get(WorkspaceFilteringParams filter, WorkspaceSorting sort)
    {
        var (items, totalCount) = await _workspaceRepository.Get(filter, sort);

        return new PagedCollection<Workspace>(items, totalCount, filter.Page, filter.PageSize);
    }
}