using Booka.Core.DTOs.Common;

namespace Booka.Core.DTOs.Workspace;

public class WorkspaceFilteringParams : PaginationParams
{
    public string? Search { get; set; }
}