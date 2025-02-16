using AutoMapper;
using Booka.Core.Domain.enums.Workspace;
using Booka.Core.DTOs.Workspace;
using Booka.Core.Interfaces.Services;
using Booka.WebApp.ApiModels.Common;
using Booka.WebApp.ApiModels.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace Booka.WebApp.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<List<WorkspaceResponse>>> Get(
        [FromQuery] WorkspaceFilteringParams filter,
        [FromQuery] WorkspaceSorting sort = WorkspaceSorting.NAME_ASC)
    {
        var result = await _workspaceService.Get(filter, sort);

        var list = _mapper.Map<List<WorkspaceResponse>>(result.Items);

        return Ok(new PagedResponse<WorkspaceResponse>(list, result.Page, result.PageSize, result.TotalCount));
    }

    [HttpGet("{workspaceId}/workplaces")]
    public async Task<ActionResult<List<WorkplaceResponse>>> GetWorkplaces(int workspaceId)
    {
        var workspace = await _workspaceService.GetByIdAsync(workspaceId);

        var result = await _workplaceService.GetWithBookingsByWorkspace(workspace.Id);

        return Ok(_mapper.Map<List<WorkplaceResponse>>(result));
    }
}