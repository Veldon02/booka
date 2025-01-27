using Booka.Core.Domain;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Services;

namespace Booka.Core.Services;

public class WorkplaceService : IWorkplaceService
{
    private readonly IWorkplaceRepository _workplaceRepository;
    private readonly IWorkspaceService _workspaceService;
    private readonly IWorkspaceRepository _workspaceRepository;

    public WorkplaceService(IWorkplaceRepository workplaceRepository, IWorkspaceService workspaceService, IWorkspaceRepository workspaceRepository)
    {
        _workplaceRepository = workplaceRepository;
        _workspaceService = workspaceService;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<List<Workplace>> GetByWorkspace(int workspaceId)
    {
        await _workspaceService.GetByIdAsync(workspaceId);

        return await _workplaceRepository.GetByWorkspace(workspaceId);
    }

    public async Task<Workplace> CreateWorkplace(int workspaceId, Workplace workplace)
    {
        var workspace = await _workspaceRepository.GetById(workspaceId, false)
            ?? throw new NotFoundException($"Workspace {workspaceId} is not found");

        workplace.Workspace = workspace;

        return await _workplaceRepository.Add(workplace);
    }
}