namespace Booka.Domain.Models;

public class Workspace : BaseEntity<int>
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string ContactEmail { get; set; }

    public string ContactPhoneNumber { get; set; }

    public IList<Workplace> Workplaces { get; set; }
}
