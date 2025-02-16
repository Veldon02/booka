namespace Booka.WebApp.ApiModels.Booking;

public class BookingResponse
{
    public int Id { get; set; }

    public DateTime BookDate { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime CreateDate { get; set; }

    public int WorkspaceId { get; set; }

    public int WorkplaceId { get; set; }
}