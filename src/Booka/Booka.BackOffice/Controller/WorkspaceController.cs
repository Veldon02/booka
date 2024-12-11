using AutoMapper;
using Booka.BackOffice.ApiModels.Workspace;
using Booka.Domain.Interfaces.Services;
using Booka.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booka.BackOffice.Controller
{
    [Route("api/workplaces")]
    [ApiController]
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

        [HttpPost]
        public async Task<ActionResult<WorkspaceResponse>> Add(AddWorkspaceRequest request)
        {
            var result = await _workspaceService.AddAsync(_mapper.Map<Workspace>(request));
            return Ok(_mapper.Map<WorkspaceResponse>(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkspaceResponse>> Update(int id, UpdateWorkspaceRequest request)
        {
            await _workspaceService.UpdateAsync(id, _mapper.Map<Workspace>(request));
            return Ok();
        }
    }
}
