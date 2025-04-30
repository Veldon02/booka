using Booka.Core.Domain.enums.Workspace;

namespace Booka.BackOffice.ApiModels.Workspace;

public class WorkspaceResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public List<WorkspaceTag> Tags { get; set; }

    public string Email { get; set; }
}