using AutoMapper;
using Booka.Core.Interfaces.Services;
using Booka.WebApp.ApiModels.Booking;
using Booka.WebApp.Filters.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Booka.WebApp.Controllers;

public class BookingController : BaseController
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public BookingController(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [OnlyCurrentUser]
    [HttpGet("users/{userId}/bookings")]
    public async Task<ActionResult<List<BookingResponse>>> Get(int userId)
    {
        var result = await _bookingService.GetUserBookings(userId);

        return Ok(_mapper.Map<List<BookingResponse>>(result));
    }

    [HttpPost("bookings")]
    public async Task<ActionResult<BookingResponse>> Create(CreateBookingRequest request)
    {
        var result = await _bookingService.Create(request.ToDomain(CurrentUserId));

        return Ok(_mapper.Map<BookingResponse>(result));
    }

    [HttpPost("workplaces/{workplaceId}/qr-scan")]
    public async Task<ActionResult<BookingResponse>> Book(int workplaceId)
    {
        var result = await _bookingService.QrScan(CurrentUserId, workplaceId);

        return Ok(_mapper.Map<BookingResponse>(result));
    }

    [HttpPatch("bookings/{bookingId}/check-in")]
    public async Task<ActionResult<BookingResponse>> CheckIn(int bookingId)
    {
        await _bookingService.CheckIn(CurrentUserId, bookingId);

        return NoContent();
    }

    [HttpDelete("bookings/{bookingId}/cancel")]
    public async Task<ActionResult<BookingResponse>> Cancel(int bookingId)
    {
        await _bookingService.Cancel(CurrentUserId, bookingId);

        return Ok();
    }
}