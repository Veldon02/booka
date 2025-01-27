using AutoMapper;
using Booka.BackOffice.ApiModels.Workplace;
using Booka.BackOffice.ApiModels.Workspace;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkspaceResponse>> GetById(int id)
    {
        var result = await _workspaceService.GetByIdAsync(id);
        return Ok(_mapper.Map<WorkspaceResponse>(result));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WorkspaceResponse>> Update(int id, UpdateWorkspaceRequest request)
    {
        await _workspaceService.UpdateAsync(id, _mapper.Map<Workspace>(request));
        return Ok();
    }

    #endregion

    #region Workplace

    [HttpGet("{workspaceId}/workplaces")]
    public async Task<ActionResult<WorkplaceResponse>> GetWorkplaces(int workspaceId)
    {
        var result = await _workplaceService.GetByWorkspace(workspaceId);

        return Ok(_mapper.Map<List<WorkplaceResponse>>(result));
    }

    [HttpPost("{workspaceId}/workplaces")]
    public async Task<ActionResult<WorkplaceResponse>> CreateWorkplace(int workspaceId, CreateWorkplaceRequest request)
    {
        var result = await _workplaceService.CreateWorkplace(workspaceId, _mapper.Map<Workplace>(request));

        return Ok(_mapper.Map<WorkplaceResponse>(result));
    }

    #endregion
}