namespace Booka.WebApp.ApiModels.Booking;

public class CreateBookingRequest
{
    public int WorkplaceId { get; set; }

    public DateTime BookDate { get; set; }

    public Core.Domain.Booking ToDomain(int userId)
    {
        return new Core.Domain.Booking
        {
            UserId = userId,
            WorkplaceId = WorkplaceId,
            BookDate = BookDate
        };
    }
}