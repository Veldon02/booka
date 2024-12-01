namespace Booka.Domain.Models;

public class User : BaseEntity<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public List<Booking> Bookings { get; set; }
}
