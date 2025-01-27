using Booka.Core.Domain.enums.Workspace;

namespace Booka.BackOffice.ApiModels.Workplace;

public class WorkplaceResponse
{
    public int Id { get; set; }

    public int Number { get; set; }

    public WorkplaceType Type { get; set; }
}