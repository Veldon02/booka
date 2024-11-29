namespace Booka.Domain.Models;

public class Booking : BaseEntity<int>
{
    public User User { get; set; }

    public Workplace Workplace { get; set; }

    public DateTime BookDate { get; set; }

    public DateTime CheckInDate { get; set; }
}
