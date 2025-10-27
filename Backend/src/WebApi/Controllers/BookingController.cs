
using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("seat-plan/{busScheduleId}")]
    public async Task<ActionResult<SeatPlanDto>> GetSeatPlan(Guid busScheduleId)
    {
        try
        {
            var result = await _bookingService.GetSeatPlanAsync(busScheduleId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize]
    [HttpPost("book")]
    public async Task<ActionResult<BookSeatResultDto>> BookSeat(BookSeatInputDto input)
    {
        var result = await _booking_service_Book(input);
        return Ok(result);
    }

    private Task<BookSeatResultDto> _booking_service_Book(BookSeatInputDto input) => _bookingService.BookSeatAsync(input);
}
