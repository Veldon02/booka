using Booka.Core.Domain;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Services;

namespace Booka.Core.Services;

public class WorkplaceService : IWorkplaceService
{
    private readonly IWorkplaceRepository _workplaceRepository;
    private readonly IQrCodeGeneratorService _qrCodeGeneratorService;

    public WorkplaceService(IWorkplaceRepository workplaceRepository, IQrCodeGeneratorService qrCodeGeneratorService)
    {
        _workplaceRepository = workplaceRepository;
        _qrCodeGeneratorService = qrCodeGeneratorService;
    }

    public async Task<List<Workplace>> GetByWorkspace(int workspaceId)
    {
        return await _workplaceRepository.GetByWorkspace(workspaceId);
    }

    public async Task<List<Workplace>> GetWithBookingsByWorkspace(int workspaceId)
    {
        return await _workplaceRepository.GetWithBookingsByWorkspace(workspaceId);
    }

    public async Task<Workplace> Create(int workspaceId, Workplace workplace)
    {
        workplace.WorkspaceId = workspaceId;

        return await _workplaceRepository.Add(workplace);
    }

    public async Task Update(int workspaceId, int workplaceId, Workplace workplace)
    {
        var existingWorkplace = await _workplaceRepository.GetById(workplaceId, false)
            ?? throw new NotFoundException($"Workplace {workplaceId} is not found");

        if (existingWorkplace.WorkspaceId != workspaceId)
        {
            throw new ForbiddenException($"You are not allowed to modify workplace {workplaceId}");
        }

        existingWorkplace.Number = workplace.Number;
        existingWorkplace.Type = workplace.Type;

        await _workplaceRepository.Update(existingWorkplace);
    }

    public async Task Delete(int workspaceId, int workplaceId)
    {
        var workplace = await _workplaceRepository.GetById(workplaceId)
            ?? throw new NotFoundException($"Workplace {workplaceId} is not found");

        if (workplace.WorkspaceId != workspaceId)
        {
            throw new ForbiddenException($"You are not allowed to delete workplace {workplaceId}");
        }

        await _workplaceRepository.Remove(workplace);
    }

    public async Task<Stream> GenerateCode(int workspaceId, int workplaceId)
    {
        var workplace = await _workplaceRepository.GetById(workplaceId)
            ?? throw new NotFoundException($"Workplace {workplaceId} is not found");

        if (workplace.WorkspaceId != workspaceId)
        {
            throw new ForbiddenException($"You are not allowed to delete workplace {workplaceId}");
        }

        return _qrCodeGeneratorService.GenerateWorkplaceBook(workplace.Id);
    }
}