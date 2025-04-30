using Booka.Core.Domain.enums.Workspace;

namespace Booka.Core.DTOs.Workspace;

public class WorkspaceDto
{
    public string? Address { get; set; }

    public string? Name { get; set; }

    public List<WorkspaceTag>? Tags { get; set; }
}