using Booka.Core.Domain;
using Booka.Core.DTOs.Common;

namespace Booka.Core.Interfaces.Services;

public interface IBookingService
{
    Task<List<Booking>> GetUserBookings(int userId);

    Task<Booking> Create(Booking booking);

    Task CheckIn(int userId, int bookingId);

    Task Cancel(int userId, int bookingId);
}