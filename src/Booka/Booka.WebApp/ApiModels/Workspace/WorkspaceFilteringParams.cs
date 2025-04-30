using Booka.Core.Domain.enums.Workspace;
using Booka.Core.DTOs.Workspace;
using Booka.WebApp.ApiModels.Common;
using Booka.WebApp.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace Booka.WebApp.ApiModels.Workspace;

public class WorkspaceFilteringParams : PaginationParams
{
    public string? Search { get; set; }

    [ModelBinder(typeof(EnumArrayQueryBinder<WorkspaceTag>))]
    public List<WorkspaceTag> Tags { get; set; } = [];

    public WorkspaceFilteringParamsDto ToDto()
    {
        return new WorkspaceFilteringParamsDto
        {
            Page = Page,
            PageSize = PageSize,
            Search = Search,
            Tags = Tags
        };
    }
}