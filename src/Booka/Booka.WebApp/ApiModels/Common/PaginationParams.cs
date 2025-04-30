using System.ComponentModel.DataAnnotations;

namespace Booka.WebApp.ApiModels.Common;

public class PaginationParams
{
    [Range(1, int.MaxValue, ErrorMessage = "Page should be greater than 0")]
    public int Page { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "PageSize should be greater than 0")]
    public int PageSize { get; set; } = 25;
}