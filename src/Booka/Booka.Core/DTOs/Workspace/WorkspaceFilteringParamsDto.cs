using Booka.Core.Domain.enums.Workspace;

namespace Booka.Core.DTOs.Workspace;

public class WorkspaceFilteringParamsDto
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public string? Search { get; set; }

    public List<WorkspaceTag> Tags { get; set; }
}