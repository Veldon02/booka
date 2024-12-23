using Booka.Core.Domain.enums;

namespace Booka.Core.Domain;

public class Workplace : BaseEntity<int>
{
    public Workspace Workspace { get; set; }

    public int Number {  get; set; }

    public WorkplaceType Type { get; set; }
}
