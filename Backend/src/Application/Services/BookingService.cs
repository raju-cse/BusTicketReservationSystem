
using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class BookingService : IBookingService
{
    private readonly IBusScheduleRepository _scheduleRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(
        IBusScheduleRepository scheduleRepository,
        ITicketRepository ticketRepository,
        IPassengerRepository passengerRepository,
        IUnitOfWork unitOfWork)
    {
        _scheduleRepository = scheduleRepository;
        _ticketRepository = ticketRepository;
        _passengerRepository = passengerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SeatPlanDto> GetSeatPlanAsync(Guid busScheduleId)
    {
        var schedule = await _scheduleRepository.GetWithDetailsAsync(busScheduleId);
        if (schedule == null)
            throw new ArgumentException("Schedule not found");

        var tickets = await _ticket_repository_GetTickets(busScheduleId);
        var seatPlan = new SeatPlanDto
        {
            BusScheduleId = busScheduleId,
            BoardingPoints = GetBoardingPoints(schedule.Route.FromCity),
            DroppingPoints = GetDroppingPoints(schedule.Route.ToCity)
        };

        foreach (var seat in schedule.Bus.Seats.OrderBy(s => s.Row).ThenBy(s => s.Column))
        {
            var ticket = tickets.FirstOrDefault(t => t.SeatId == seat.Id);
            seatPlan.Seats.Add(new SeatDto
            {
                SeatId = seat.Id,
                SeatNumber = seat.SeatNumber,
                Row = seat.Row,
                Column = seat.Column,
                Status = ticket?.Status.ToString() ?? TicketStatus.Available.ToString()
            });
        }

        return seatPlan;
    }

    private Task<List<Ticket>> _ticket_repository_GetTickets(Guid busScheduleId)
    {
        return _ticketRepository.GetTicketsForScheduleAsync(busScheduleId);
    }

    public async Task<BookSeatResultDto> BookSeatAsync(BookSeatInputDto input)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var isAvailable = await _ticketRepository.IsSeatAvailableAsync(input.BusScheduleId, input.SeatId);
            if (!isAvailable)
            {
                return new BookSeatResultDto { Success = false, Message = "Seat is already booked" };
            }

            var passenger = await _passengerRepository.GetByMobileAsync(input.MobileNumber);
            if (passenger == null)
            {
                // create passenger with blank password (user should signup)
                passenger = new Passenger(input.PassengerName, input.MobileNumber, "");
                await _passenger_repository_Add(passenger);
            }

            var ticket = new Ticket(input.BusScheduleId, input.SeatId, passenger.Id, input.BoardingPoint, input.DroppingPoint);
            await _ticket_repository_Add(ticket);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new BookSeatResultDto 
            { 
                Success = true, 
                TicketId = ticket.Id,
                Message = "Seat booked successfully" 
            };
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private Task _passenger_repository_Add(Passenger p) => _passengerRepository.AddAsync(p);
    private Task _ticket_repository_Add(Ticket t) => _ticketRepository.AddAsync(t);

    private List<string> GetBoardingPoints(string fromCity)
    {
        return new List<string>
        {
            $"{fromCity} Counter",
            $"{fromCity} Bus Stand",
            $"{fromCity} Terminal"
        };
    }

    private List<string> GetDroppingPoints(string toCity)
    {
        return new List<string>
        {
            $"{toCity} Counter",
            $"{toCity} Bus Stand",
            $"{toCity} Terminal"
        };
    }
}
