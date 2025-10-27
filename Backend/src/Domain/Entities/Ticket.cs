
namespace Domain.Entities;

public class Ticket : BaseEntity
{
    public Guid BusScheduleId { get; private set; }
    public Guid SeatId { get; private set; }
    public Guid PassengerId { get; private set; }
    public string BoardingPoint { get; private set; }
    public string DroppingPoint { get; private set; }
    public TicketStatus Status { get; private set; }
    public DateTime BookingDate { get; private set; }

    public BusSchedule BusSchedule { get; private set; }
    public Seat Seat { get; private set; }
    public Passenger Passenger { get; private set; }

    private Ticket() { }

    public Ticket(Guid busScheduleId, Guid seatId, Guid passengerId, string boardingPoint, string droppingPoint)
    {
        BusScheduleId = busScheduleId;
        SeatId = seatId;
        PassengerId = passengerId;
        BoardingPoint = boardingPoint;
        DroppingPoint = droppingPoint;
        Status = TicketStatus.Booked;
        BookingDate = DateTime.UtcNow;
    }

    public void ConfirmBooking()
    {
        Status = TicketStatus.Sold;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CancelBooking()
    {
        Status = TicketStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}
