using AutoMapper;
using Booka.BackOffice.ApiModels.Workplace;
using Booka.BackOffice.ApiModels.Workspace;
using Booka.BackOffice.Filters.Attributes;
using Booka.Core.Domain;
using Booka.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booka.BackOffice.Controller;

[Route("api/workspaces")]
public class WorkspaceController : BaseController
{
    private readonly IWorkspaceService _workspaceService;
    private readonly IWorkplaceService _workplaceService;
    private readonly IMapper _mapper;

    public WorkspaceController(IWorkspaceService workspaceService, IMapper mapper, IWorkplaceService workplaceService)
    {
        _workspaceService = workspaceService;
        _mapper = mapper;
        _workplaceService = workplaceService;
    }

    #region Workspace

    [OnlyLoggedWorkspace]
    [HttpGet("{workspaceId}")]
    public async Task<ActionResult<WorkspaceResponse>> GetById(int workspaceId)
    {
        var result = await _workspaceService.GetByIdAsync(workspaceId);
        return Ok(_mapper.Map<WorkspaceResponse>(result));
    }

    [OnlyLoggedWorkspace]
    [HttpPut("{workspaceId}")]
    public async Task<ActionResult<WorkspaceResponse>> Update(int workspaceId, UpdateWorkspaceRequest request)
    {
        await _workspaceService.UpdateAsync(workspaceId, _mapper.Map<Workspace>(request));
        return Ok();
    }

    #endregion

    #region Workplace

    [OnlyLoggedWorkspace]
    [HttpGet("{workspaceId}/workplaces")]
    public async Task<ActionResult<WorkplaceResponse>> GetWorkplaces(int workspaceId)
    {
        var result = await _workplaceService.GetByWorkspace(workspaceId);

        return Ok(_mapper.Map<List<WorkplaceResponse>>(result));
    }

    [OnlyLoggedWorkspace]
    [HttpPost("{workspaceId}/workplaces")]
    public async Task<ActionResult<WorkplaceResponse>> CreateWorkplace(int workspaceId, CreateWorkplaceRequest request)
    {
        var result = await _workplaceService.Create(workspaceId, _mapper.Map<Workplace>(request));

        return Ok(_mapper.Map<WorkplaceResponse>(result));
    }

    [HttpPut("/api/workplaces/{workplaceId}")]
    public async Task<ActionResult<WorkplaceResponse>> UpdateWorkplace(int workplaceId, UpdateWorkplaceRequest request)
    {
        await _workplaceService.Update(CurrentUserId, workplaceId, _mapper.Map<Workplace>(request));

        return NoContent();
    }

    [HttpDelete("/api/workplaces/{workplaceId}")]
    public async Task<ActionResult<WorkplaceResponse>> DeleteWorkplace(int workplaceId)
    {
        await _workplaceService.Delete(CurrentUserId, workplaceId);

        return Ok();
    }

    [HttpGet("/api/workplaces/{workplaceId}/generate-qr")]
    public async Task<ActionResult> GenerateCode(int workplaceId)
    {
        var qrCode = await _workplaceService.GenerateCode(CurrentUserId, workplaceId);

        return File(qrCode, "image/png", $"qr-workplace-{workplaceId}.png");
    }

    #endregion
}