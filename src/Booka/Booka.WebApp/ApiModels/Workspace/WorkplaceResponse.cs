using Booka.Core.Domain.enums.Workspace;

namespace Booka.WebApp.ApiModels.Workspace;

public class WorkplaceResponse
{
    public int Id { get; set; }

    public int Number { get; set; }

    public WorkplaceType Type { get; set; }

    public bool IsAvailable { get; set; }
}