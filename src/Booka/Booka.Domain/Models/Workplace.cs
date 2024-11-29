using Booka.Domain.Enums;

namespace Booka.Domain.Models;

public class Workplace : BaseEntity<int>
{
    public Workspace Workspace { get; set; }

    public int Number {  get; set; }

    public WorkplaceType Type { get; set; }
}
