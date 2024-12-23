using AutoMapper;
using Booka.BackOffice.ApiModels.Workspace;
using Booka.Core.Domain;
using Booka.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booka.BackOffice.Controller;

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
    public async Task<ActionResult<List<Workspace>>> GetAll()
    {
        var result = await _workspaceService.GetAllAsync();
        return Ok(_mapper.Map<List<WorkspaceResponse>>(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Workspace>>> GetById(int id)
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
}