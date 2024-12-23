using AutoMapper;
using Booka.Core.Interfaces.Services;
using Booka.WebApp.ApiModels.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace Booka.WebApp.Controllers;

[Route("api/workspaces")]
public class WorkspaceController : ControllerBase
{
    private readonly IWorkspaceService _workspaceService;
    private readonly IMapper _mapper;

    public WorkspaceController(IWorkspaceService workspaceService, IMapper mapper)
    {
        _workspaceService = workspaceService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkspaceResponse>>> GetAll()
    {
        var result = await _workspaceService.GetAllAsync();

        return Ok(_mapper.Map<List<WorkspaceResponse>>(result));
    }
}