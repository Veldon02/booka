using Booka.Core.Domain.enums.Workspace;

namespace Booka.Core.Domain;

public class Workplace : BaseEntity<int>
{
    public int WorkspaceId {get; set; }

    public Workspace Workspace { get; set; }

    public int Number {  get; set; }

    public WorkplaceType Type { get; set; }
}
