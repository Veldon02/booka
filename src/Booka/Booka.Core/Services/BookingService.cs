using Booka.Core.Domain;
using Booka.Core.DTOs.Common;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Services;

namespace Booka.Core.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IWorkplaceRepository _workplaceRepository;

    public BookingService(IBookingRepository bookingRepository,
        IUserRepository userRepository,
        IWorkplaceRepository workplaceRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _workplaceRepository = workplaceRepository;
    }

    public async Task<List<Booking>> GetUserBookings(int userId)
    {
        return await _bookingRepository.GetByUser(userId);
    }

    public async Task<Booking> Create(Booking booking)
    {
        // This check need to be here to handle the timezones
        // conversions later in the future
        if (booking.BookDate.Date < DateTime.UtcNow.Date)
        {
            throw new InvalidParametersException("Book date can not be less than the current date");
        }

        var user = await _userRepository.GetById(booking.UserId, false) 
                   ?? throw new NotFoundException($"User {booking.UserId} is not found");

        var workplace = await _workplaceRepository.GetByIdWithBookings(booking.WorkplaceId, false) 
                        ?? throw new NotFoundException($"Workplace {booking.WorkplaceId} is not found");
        // TODO handle the case with concurrent calls 
        if (!workplace.IsAvailable(booking.BookDate))
        {
            throw new BookConflictException("Workplace is not available");
        }

        var result = await _bookingRepository.Add(booking);

        result.Workplace = workplace;

        return result;
    }

    public async Task CheckIn(int userId, int bookingId)
    {
        var booking = await _bookingRepository.GetById(bookingId, false)
                      ?? throw new NotFoundException($"Booking {bookingId} is not found");

        if (booking.UserId != userId)
        {
            throw new ForbiddenException("You can not access this workplace");
        }

        if (booking.CheckInDate is not null)
        {
            throw new InvalidParametersException($"Booking {bookingId} is already checked in");
        }

        booking.CheckInDate = DateTime.UtcNow;
        await _bookingRepository.Update(booking);
    }

    public async Task Cancel(int userId, int bookingId)
    {
        var booking = await _bookingRepository.GetById(bookingId, false)
                      ?? throw new NotFoundException($"Booking {bookingId} is not found");

        if (booking.UserId != userId)
        {
            throw new ForbiddenException("You can not access this workplace");
        }

        if (booking.CheckInDate is not null)
        {
            throw new InvalidParametersException($"Booking {bookingId} can not be canceled");
        }

        await _bookingRepository.Remove(booking);
    }
}