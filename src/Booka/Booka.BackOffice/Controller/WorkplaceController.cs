using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booka.BackOffice.Controller
{
    [Route("api/workplaces")]
    [ApiController]
    public class WorkplaceController : ControllerBase
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkplaceController(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<Workspace>>> GetAll()
        {
            return await _workspaceRepository.GetAll();
        }
    }
}
