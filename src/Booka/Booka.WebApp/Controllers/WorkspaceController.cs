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
    private readonly IMapper _mapper;

    public WorkspaceController(IWorkspaceService workspaceService, IMapper mapper)
    {
        _workspaceService = workspaceService;
        _mapper = mapper;
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
}