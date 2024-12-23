namespace Booka.Core.Domain;

public class Workspace : BaseEntity<int>
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public IList<Workplace> Workplaces { get; set; }
}
