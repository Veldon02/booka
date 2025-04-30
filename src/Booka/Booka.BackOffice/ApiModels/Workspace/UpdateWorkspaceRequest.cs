using Booka.Core.Domain.enums.Workspace;
using Booka.Core.DTOs.Workspace;

namespace Booka.BackOffice.ApiModels.Workspace;

public class UpdateWorkspaceRequest
{
    public string? Name { get; set; }

    public string? Address { get; set; }

    public List<WorkspaceTag>? Tags { get; set; }

    public WorkspaceDto ToDto()
    {
        return new WorkspaceDto()
        {
            Name = Name,
            Address = Address,
            Tags = Tags,
        };
    }
}